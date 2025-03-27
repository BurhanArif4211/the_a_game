using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform player;            // The player character
    public Transform cameraTransform;   // Camera transform

    [Header("Settings")]
    public Vector3 offset = new Vector3(0, 1.5f, -3f); // Camera position relative to player
    public float rotationSpeed = 3f;    // Mouse sensitivity
    public float followSmoothness = 5f; // Camera follow smoothness

    [Header("View Angles")]
    public float minVerticalAngle = -20f;
    public float maxVerticalAngle = 60f;

    private float currentX = 0f;
    private float currentY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Mouse input to freely rotate camera
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentY = Mathf.Clamp(currentY, minVerticalAngle, maxVerticalAngle);

        // Apply rotation to camera
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = player.position + rotation * offset;

        // Smoothly follow player but don't rotate with movement
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, followSmoothness * Time.deltaTime);
        cameraTransform.LookAt(player.position + Vector3.up * 1.5f); // Look at player’s upper body
    }
}
//old deepseek code

//using UnityEngine;

//public class ThirdPerson : MonoBehaviour
//{
//    [Header("References")]
//    public Transform player;            // The player's transform
//    public Transform pivot;             // A pivot point for camera rotation (optional)
//    public Transform cameraTransform;   // The actual camera transform

//    [Header("Positioning")]
//    public float distanceFromPlayer = 5f; // Distance behind the player
//    public float height = 2f;            // Height above the player
//    public float cameraSmoothness = 5f;  // How smoothly the camera follows

//    [Header("Rotation")]
//    public float rotationSpeed = 3f;     // Mouse rotation sensitivity
//    public float minVerticalAngle = -20f; // How far down you can look
//    public float maxVerticalAngle = 60f;  // How far up you can look

//    private float mouseX, mouseY;        // Mouse input values

//    void Start()
//    {
//        // Hide and lock cursor
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;

//        // Initialize camera position if no pivot is set
//        if (pivot == null)
//        {
//            GameObject pivotObj = new GameObject("Camera Pivot");
//            pivot = pivotObj.transform;
//            pivot.SetParent(player);
//        }
//    }

//    void LateUpdate()
//    {
//        if (player == null) return;

//        // Get mouse input
//        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
//        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

//        // Clamp vertical rotation
//        mouseY = Mathf.Clamp(mouseY, minVerticalAngle, maxVerticalAngle);

//        // Rotate pivot and player (for character to face camera forward)
//        pivot.rotation = Quaternion.Euler(mouseY, mouseX, 0);
//        //player.rotation = Quaternion.Euler(0, mouseX, 0);

//        // Calculate desired camera position
//        Vector3 desiredPosition = pivot.position - pivot.forward * distanceFromPlayer + Vector3.up * height;

//        // Smoothly move camera to desired position
//        cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, cameraSmoothness * Time.deltaTime);

//        // Make camera look at pivot point
//        cameraTransform.LookAt(pivot.position);
//    }
//}