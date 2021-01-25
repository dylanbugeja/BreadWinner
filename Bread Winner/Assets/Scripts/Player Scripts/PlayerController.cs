using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControlMap playerControlMap;
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;

    private Collider2D col;

    private bool facingRight;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerControlMap = new PlayerControlMap();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        playerControlMap.Enable();
    }
    private void OnDisable()
    {
        playerControlMap.Disable();
    }
    void Start()
    {
        playerControlMap.Land.Jump.performed += _ => Jump();
    }

    private void Jump()
    {
        if (IsGrounded())
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
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

    void Update()
    {
        //Reads the movement value
        float moveInput = playerControlMap.Land.Move.ReadValue<float>();

        //Get player current position
        Vector3 currentPos = transform.position;
        //Move the player
        currentPos.x += moveInput * speed * Time.deltaTime;
        transform.position = currentPos;
    }
}
