using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSuperPlayerM : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;
    public GameObject fix;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        //ground check
       // grounded = Physics.Raycast(fix.transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        RaycastTeST();    

        SpeedControl();
        MyInput();

        //handle drag
        if (grounded==true)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //I say Jump
        if(Input.GetKey(jumpKey)&&readyToJump&&grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //Dirección xd
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //En el Suelo
        if(grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //Aire
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatvel.magnitude>moveSpeed)
        {
            Vector3 limitedVel = flatvel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }


    void RaycastTeST()
    {
       
        
        if (Physics.Raycast(fix.transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround))
        {
            Debug.Log("Le di");
            grounded=true;
            Debug.DrawRay(fix.transform.position, Vector3.down, Color.green);
        }
        else
        {
            grounded = false;
            Debug.DrawRay(fix.transform.position, Vector3.down, Color.red);
        }
    }
}
