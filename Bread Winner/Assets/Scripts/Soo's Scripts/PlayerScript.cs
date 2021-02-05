using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float movePower = 5f;
    public float jumpPower = 5f;
    public float jumpTimeLimit = 0.1f;
    public int maxHealth = 2;

    Rigidbody2D rigid;

    float mx;

    Vector3 movement;
    bool isDie = false;
    bool isJumping = false;

    int health = 1;

    GrapplingHook grappling;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();

        health = maxHealth;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if (health == 0)
        {
            if (!isDie)
                Die();

            return;
        }
    }

    void FixedUpdate()
    {
        if (health == 0)
            return;

        Move();
        Jump();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        mx = Input.GetAxisRaw("Horizontal");

        //if (grappling.isAttach)
        //{
            //rigid.AddForce(movePower, ForceMode2D.Impulse);
        //}

        if (mx < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            moveVelocity = Vector3.left;
        }
        else if (mx > 0) 
        {
            transform.localScale = new Vector3(1, 1, 1);
            moveVelocity = Vector3.right;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump()
    {
        if (!isJumping)
            return;

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    void Die()
    {
        isDie = true;

        rigid.velocity = Vector2.zero;

        BoxCollider2D[] colls = gameObject.GetComponents<BoxCollider2D>();
        colls[0].enabled = false;
        colls[1].enabled = false;

        Vector2 dieVelocity = new Vector2(0, 10f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "obstacle"){

            Vector2 attackedVelocity = Vector2.zero;
            if (other.gameObject.transform.position.x > transform.position.x)
                attackedVelocity = new Vector2(-2f, 7f);
            else
                attackedVelocity = new Vector2(2f, 7f);

            health--;
        }

        if(other.gameObject.tag == "Coin")
        {
            BuffStatus coin = other.gameObject.GetComponent<BuffStatus>();
            movePower += coin.value;

            Destroy(other.gameObject, 0f);
        }

        if (other.gameObject.tag == "Bag")
        {
            health++;

            Destroy(other.gameObject, 0f);
        }

        if (other.gameObject.tag == "MainCamera")
        {
            Vector2 attackedVelocity = Vector2.zero;
            if (other.gameObject.transform.position.x > transform.position.x)
                attackedVelocity = new Vector2(-20f, 20f);
            else
                attackedVelocity = new Vector2(20f, 20f);

            health--;
        }

        if(other.gameObject.tag == "Bottom")
        {
            Destroy(gameObject, 0f);
        }
    }
}
