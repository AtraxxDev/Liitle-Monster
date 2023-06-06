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

    [Header("PullStuff")]
    public bool Freeze;
    public bool ActiveGrapple;
    public GameObject Arm;
    PullObject Pull;

    public Transform orientation;
    public GameObject fix;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    Rigidbody rb;
    Animator animator;

    public GameObject Player;
    public Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Pull = Arm.GetComponent<PullObject>();
        rb.freezeRotation = true;
        readyToJump = true;

<<<<<<< Updated upstream
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true); // Inicia la animación de idle
=======
        Player = GameObject.FindGameObjectWithTag("ModelPlayer");
        anim = Player.GetComponent<Animator>();

>>>>>>> Stashed changes
    }

    private void Update()
    {
        // Ground check
        grounded = Physics.Raycast(fix.transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        // Handle drag
        if (grounded && !ActiveGrapple)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (Freeze)
        {
            rb.velocity = Vector3.zero;
        }
<<<<<<< Updated upstream
=======

        if(ActiveGrapple)
            anim.SetBool("Grappling", true);
        else
            anim.SetBool("Grappling", false);

        if (rb.velocity!=Vector3.zero&&grounded)
        {
            anim.SetBool("IsWalking", true);
        }
        else
            anim.SetBool("IsWalking", false);

        if (!grounded&&!ActiveGrapple)
        {
            anim.SetBool("IsJumping", true);
        }
        else
            anim.SetBool("IsJumping", false);






>>>>>>> Stashed changes
    }

   

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jump input
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();
            Pull.GetComponent<PullObject>().StopGrapple();
        }
    }

    public void ResetRestrictions()
    {
        ActiveGrapple = false;
    }

    private void MovePlayer()
    {
        if (ActiveGrapple) return;

        // Dirección
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

<<<<<<< Updated upstream
        // En el suelo
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            if (moveDirection != Vector3.zero)
            {
                animator.SetBool("Idle", false); // Deja de reproducir la animación de idle
                animator.SetBool("Walk", true); // Reproduce la animación de walk
            }
            else
            {
                animator.SetBool("Walk", false); // Deja de reproducir la animación de walk
                animator.SetBool("Idle", true); // Reproduce la animación de idle
            }
        }
        // En el aire
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            animator.SetBool("Idle", false); // Deja de reproducir la animación de idle
            animator.SetBool("Walk", false); // Deja de reproducir la animación de walk
        }
=======
        //En el Suelo
        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            anim.SetBool("IsMoving", true);
        }
        

        //Aire
        else if (!grounded)
        {
            anim.SetBool("IsMoving", false);
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
            
>>>>>>> Stashed changes
    }

    private void SpeedControl()
    {
        if (ActiveGrapple) return;

        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Limitar la velocidad si es necesario
        if (flatvel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatvel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
<<<<<<< Updated upstream
        animator.SetBool("Jump", true); // Reproduce la animación de jump
=======
        
>>>>>>> Stashed changes
    }

    private void ResetJump()
    {
        readyToJump = true;
        animator.SetBool("Jump", false); // Deja de reproducir la animación de jump
    }

    void RaycastTeST()
    {
        if (Physics.Raycast(fix.transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround))
        {
            grounded = true;
            Debug.DrawRay(fix.transform.position, Vector3.down, Color.green);
        }
        else
        {
            grounded = false;
            Debug.DrawRay(fix.transform.position, Vector3.down, Color.red);
        }
    }

    public bool enableMovementOnNextTouch;

    public void JumpToBitches(Vector3 targetPosition, float trajectoryHeight)
    {
        ActiveGrapple = true;
        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f); // La velocidad se aplica después de 0.1 segundos
        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;

    private void SetVelocity()
    {
        if (!Application.isPlaying) return;
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }
}


