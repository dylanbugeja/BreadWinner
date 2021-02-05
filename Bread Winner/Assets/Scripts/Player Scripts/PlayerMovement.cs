using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //the_bread_association3701

    private float inputX;
    private Collider2D col;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int playerIndex = 0;
    [SerializeField] private int health = 1;

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
       rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
    }
    public void Jump(bool performed)
    {
        if (performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    public void Grapple()
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
}
