using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public bool Freeze;
    public bool ActiveGrapple;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;
    private bool readyToJump;
    private Vector3 velocity;
    public float jumpCooldown;

    //Ivan add-on
   // private Vector3 normalVector = Vector3.up;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        readyToJump = true;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity;
    }

    private void Update()
    {
        
        Move();
        Jump2();

        if(Freeze)
        {
            rb.velocity = Vector3.zero;
        }

    }

    public void ResetRestrictions()
    {
        ActiveGrapple = false;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<PullObject>().StopGrapple();
        }
    }

    private void Move()
    {
        if(ActiveGrapple) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // Rotación del personaje hacia la dirección de movimiento
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

        float speed = isRunning ? runSpeed : walkSpeed;

        velocity.x = movement.x * speed;
        velocity.z = movement.z * speed;
        
    }

    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f; // Restablece la velocidad vertical cuando toca el suelo
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //rb.AddForce(Vector2.up * jumpForce * 1.5f);
            // rb.AddForce(normalVector * jumpForce * 0.5f);
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity); // Cálculo del salto
        }

        velocity.y += gravity * Time.deltaTime; // Aplica la gravedad
        rb.velocity = velocity;
    }

    private void Jump2()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButtonDown("Jump") && isGrounded&&readyToJump)
        {
            Debug.Log("Salte");
            readyToJump = false;

            velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    

    public bool enableMovementOnNextTouch;

    public void JumpToBitches(Vector3 targetPosition,float trajectoryHeight)
    {
        ActiveGrapple = true;

        velocityToSet= CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f); //La Velocidad se aplica después de 0.1 segundos

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;

    private void SetVelocity()
    {
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
