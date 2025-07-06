using UnityEngine;
using UnityEngine.InputSystem;
/*
[RequireComponent(typeof(CharacterController))]
public class playerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float groundDistance = 0.2f; // Distance to check for ground

    private Rigidbody rb;
    private @InputActionSystem control;
    private Vector2 moveInput;
    private Vector3 moveDirection;
    private bool isGrounded;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from the player object.");
        }
        control = new @playerControl();
        if (controls == null)
        {
            Debug.LogError("Player controls are not initialized.");
            return;
        }
        control.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        control.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        control.Player.Jump.performed += Jump;
    }
    void OnEnable()
    {
        if (control != null)
        {
            control.Enable();
        }
        else
        {
            Debug.LogError("Player controls are not initialized.");
        }
    }
    void OnDisable()
    {
        if (control != null)
        {
            control.Disable();
        }
        else
        {
            Debug.LogError("Player controls are not initialized.");
        }
    }

    void FixedUpdate()
    {
        //CheckGrounded();
        HandleMovement();
    }
    private void CheckGrounded()
    {
        // Check if the player is grounded using a raycast
        RaycastHit hitInfo;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hitInfo, groundDistance);
        if (isGrounded)
        {
            Debug.Log("Player is grounded.");
        }
        else
        {
            Debug.Log("Player is not grounded.");
        }
    }
    void HandleMovement()
    {
        moveDirection = 
            Camera.main.transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y));
        rb.linearVelocity = moveSpeed * moveDirection * Time.fixedDeltaTime + new Vector3(0, rb.linearVelocity.y, 0);
    }



    void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump action performed with context: " + context);
        if (isGrounded && context.performed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            Debug.Log("Jumped");
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Grounded");
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("Left ground");
        }
    }
}
*/