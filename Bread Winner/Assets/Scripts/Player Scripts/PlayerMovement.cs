using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //the_bread_association3701
    public Projectile projectilePrefab;
    public Projectile bombProjectilePrefab;
    public Transform launchOffset;

    private string currentState;
    private Animator animator;
    private float inputX;
    private Collider2D col;

    private GameManager gm;
    public float prinputValue;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float jumpForce = 11f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int playerIndex = 0;
    [SerializeField] private int health = 1;



    public ParticleSystem dust;
    public float moveTimer;
    public bool isBoosting;
    public bool isSlowing;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gm.players.Add(gameObject);
        gm.totalPlayers++;
        if (gm.totalPlayers == 1)
        {
            gm.leadPlayer = gameObject;
        }
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }
    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    private void Update()
    {
        Move();
        moveSpeed += ((float)gm.time / 100000);
        Debug.Log(moveSpeed);
    }
    public void SetInputValue(Vector2 inputValue)
    {
        inputX = inputValue.x;
    }
    private void Move()
    {

        if (IsGrounded())
        {

            if (inputX < 0 && prinputValue != inputX)
            {
                ChangeAnimation("Run");
                dust.Play();
                //transform.Rotate(0f, 180f, 0f);
                transform.rotation = new Quaternion(0, 1, 0, 0);
                prinputValue = inputX;
                //transform.localScale = new Vector3(-1, 1, 1);
            }
            if (inputX > 0 && prinputValue != inputX)
            {
                dust.Play();
                ChangeAnimation("Run");
                //transform.Rotate(0f, 180f, 0f);
                transform.rotation = new Quaternion(0, 0, 0, 0);
                prinputValue = inputX;
                //transform.localScale = new Vector3(1, 1, 1);
                
            }
            if (inputX == 0 && prinputValue != inputX)
            {
                dust.Stop();
                ChangeAnimation("Idle");
            }
            
        }
        if (isBoosting)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer >= 2)
            {
                moveSpeed-= 4f;
                moveTimer = 0;
                isBoosting = false;
            }
        }
        if (isSlowing)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer >= 3)
            {
                moveSpeed+= 4f;
                moveTimer = 0;
                isSlowing = false;
            }
        }
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
    }
    public void Jump(bool performed)
    {

        if (performed && IsGrounded())
        {
            dust.Stop();
            ChangeAnimation("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    public void Grapple(bool performed)
    {
        if (performed)
        {
            Debug.Log(Inventory.instance.slots[0].slotObj.name);
        }
    }
    public void Use(bool performed)
    {
        if (performed)
        {
            Instantiate(projectilePrefab, launchOffset.position, transform.rotation);
            Instantiate(bombProjectilePrefab, launchOffset.position, transform.rotation);
        }
    }
    private bool IsGrounded()
    { 
        Vector2 tlPoint = transform.position;
        tlPoint.x -= col.bounds.extents.x;
        tlPoint.y += col.bounds.extents.y;

        Vector2 brPoint = transform.position;
        brPoint.x += col.bounds.extents.x;
        brPoint.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(tlPoint, brPoint, ground);
    }
    public void ReceiveDamage(string objectTag)
    {
        //Get the object tag from the collision allowing different if statements based on what it collides into
        health--;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Inventory inven = other.GetComponent<Inventory>();
        if (other.gameObject.tag == "Obstacle")
        {
            health--;
            if (health == 0)
            {
                Die();
            }
        }

        if (other.gameObject.tag == "SpeedBoost")
        {
            moveSpeed+= 4f;
            isBoosting = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "SandwichBag")
        {
            health++;

            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Trap")
        {
            moveSpeed-= 4f;
            isSlowing = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "Destroyer_bottom" || other.gameObject.name == "Destroyer_left")
        {
            Die();
        }
    }
    void Die()
    {
        gm.totalPlayers--;
        gm.players.Remove(gameObject);
        gm.CheckGameOver();
        Destroy(gameObject);
    }
    public string getCharacter()
    {
        return GetComponent<PlayerInputHandler>().GetCharacter();
    }
    private void ChangeAnimation(string newState)
    {
        if (currentState == newState) return; //Dylan: Stop animator from interrupting


        animator.Play(newState); //Dylan: Play new Animation

        currentState = newState; //reassign the current state
    }
    void CreateDust()
    {
        dust.Play();
    }
}
