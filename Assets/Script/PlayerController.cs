using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private bool faceMoveDirection = false;

    private float moveSpeed = 5f;
    private CharacterController controller;
    private Animator animator;
    private Vector2 moveInput;
    private Vector3 velocity;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (controller == null){Debug.LogError("CharacterController component is missing on the player object.");}
        if (animator == null){Debug.LogError("Animator component is missing on the player object.");}
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("speed",moveInput.magnitude);
        if ( moveInput==null){Debug.LogError("Move input is null.");return;}
        Debug.Log($"Move input: {moveInput}");
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveSpeed = sprintSpeed;
            animator.SetFloat("speed", moveInput.magnitude*2);
        }
        else if (context.canceled)
        {
            moveSpeed = walkSpeed;
            animator.SetFloat("speed", moveInput.magnitude);
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"{context.performed}, is grounded: {controller.isGrounded}");
        animator.SetBool("isGrounded", controller.isGrounded);
        if (context.performed && controller.isGrounded)
        {
            Debug.Log("Jump performed.");
            velocity.y =Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }
    void Update()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        if (faceMoveDirection && moveDirection.sqrMagnitude>0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
