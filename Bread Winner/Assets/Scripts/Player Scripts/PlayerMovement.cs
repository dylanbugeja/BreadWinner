using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    //the_bread_association3701

    private float inputX;
    private Collider2D col;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int playerIndex = 0;
    [SerializeField] private int health = 1;

    public float boostingTimer;
    public bool isBoosting;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        Move();
    }
    public int GetPlayerIndex()
    {
        return playerIndex;
    }
    public void SetInputValue(Vector2 inputValue)
    {
        inputX = inputValue.x;
    }
    private void Move()
    {
        if (IsGrounded()) {
           if(inputX < 0) transform.localScale = new Vector3(-1, 1, 1);
           if(inputX > 0) transform.localScale = new Vector3(1, 1, 1);
        }
        if (isBoosting)
        {
            boostingTimer += Time.deltaTime;
            if(boostingTimer >= 5)
            {
                moveSpeed = 6f;
                boostingTimer = 0;
                isBoosting = false;
            }
        }
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
    }
    public void Jump(bool performed)
    {
        if (performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    public void Grapple(bool performed)
    {
        if (performed)
        {
            Debug.Log(Inventory.instance.slots[0].slotObj);
        }
    }
    public void Use(bool performed)
    {

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
            if(health == 0)
            {
                Destroy(gameObject);
                GameManager.instance.GameOver();
            }
        }

        if (other.gameObject.tag == "SpeedBoost")
        {
            moveSpeed = 12f;
            isBoosting = true;
        }

        if (other.gameObject.tag == "SandwichBag")
        {
            health++;

            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "Destroyer_bottom" || other.gameObject.name == "Destroyer_left")
        {
            Destroy(gameObject);
            GameManager.instance.GameOver();
        }
    }
}
