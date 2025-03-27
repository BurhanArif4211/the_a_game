using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform player;            // The player character
    public Transform cameraTransform;   // Camera transform

    [Header("Settings")]
    public Vector3 offset = new Vector3(0, 1.5f, -3f); // Camera position relative to player
    public float rotationSpeed = 3f;    // Mouse sensitivity
    public bool SmoothFollow = true;    // Smooth camera follow
    public float followSmoothness = 3f; // Camera follow smoothness

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

        if (SmoothFollow)
        {
            // Smoothly follow player and rotate with movement
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, followSmoothness * Time.deltaTime);
        }
        else
        {
            // Instantly follow player and rotate with movement
            cameraTransform.position = desiredPosition;
        }
            // Smoothly follow player but don't rotate with movement
            //cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, followSmoothness * Time.deltaTime);
        cameraTransform.LookAt(player.position + Vector3.up * 1.5f); // Look at player’s upper body
    }
}
