using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        Move();
        Jump();
    }

    private void CheckGrounded()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * horizontalInput + transform.forward * verticalInput;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Puedo saltar");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
