using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float zoomLerpSpeed = 5f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 15f;

    private InputActionSystem control;
    private CinemachineCamera cam;
    private CinemachineOrbitalFollow orbital;
    private Vector2 ScrollDelta;
    private float targetDistance;
    private float currentDistance;
    
    void Start()
    {
        control = new InputActionSystem();
        if (control == null )
        {
            Debug.LogError("Cannot get the InputAction for control");
            return;
        }
        control.Enable();
        control.CameraControl.MouseScroll.performed += HandleMouseScroll;
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<CinemachineCamera>();
        if (cam == null)
        {
            Debug.LogError("CinemachineCamera component not found on this GameObject.");
            return;
        }
        orbital = cam.GetComponent<CinemachineOrbitalFollow>();
        targetDistance = currentDistance = orbital.Radius;
    }

    private void HandleMouseScroll(InputAction.CallbackContext context)
    {
        ScrollDelta = context.ReadValue<Vector2>();
        //Debug.Log($"Mouse is scrolling. ScrollDelta: {ScrollDelta}");
        targetDistance -= ScrollDelta.y * zoomSpeed;
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (ScrollDelta.y != 0)
        {
            targetDistance = Mathf.Clamp(orbital.Radius - ScrollDelta.y*zoomSpeed, minDistance, maxDistance);
            ScrollDelta = Vector2.zero;
        }
        float bumperDelta = control.CameraControl.GamepadScroll.ReadValue<float>();
        if (bumperDelta != 0)
        {
            targetDistance = Mathf.Clamp(orbital.Radius - bumperDelta*zoomSpeed, minDistance, maxDistance);
        }



        currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * zoomLerpSpeed);
        orbital.Radius = currentDistance;
    }
}
