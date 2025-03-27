using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public  float normalSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float acceleration = 5f; // Speed smoothing
    [SerializeField] private float deceleration = 8f; // Stops faster when no input
    [SerializeField] private float rotationSpeed = 15f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator animator;

    private Vector3 moveDirection;
    private float currentSpeed = 0f;
    private bool isWalking = false;
    private bool isSprinting = false;

    void Update()
    {
        HandleMovement();
        HandleAnimation();
    }

    private void HandleMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        isWalking = (inputX != 0 || inputZ != 0);
        isSprinting = Input.GetKey(KeyCode.LeftShift) && isWalking;

        // Determine target speed (sprinting or walking)
        float targetSpeed = isSprinting ? sprintSpeed : normalSpeed;
        //Debug.Log("MovementIntensity " + GetMovementIntensity());

        if (isWalking)
        {
            moveDirection = cameraTransform.forward * inputZ + cameraTransform.right * inputX;
            moveDirection.y = 0;
            moveDirection.Normalize();

            RotateTowardsDirection(moveDirection);

            // Smooth speed transition (acceleration)
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);
        }
        else
        {
            // Smooth stopping (deceleration)
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * deceleration);
        }

        // Apply movement
        transform.position += moveDirection * currentSpeed * Time.deltaTime;
    }

    public void SetSpeed(float speed)
    {
        normalSpeed = speed;
    }
    private void RotateTowardsDirection(Vector3 direction)
    {
        if (direction.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleAnimation()
    {
        float movementIntensity = GetMovementIntensity();

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isSprinting", isSprinting);
        animator.SetFloat("playerVelocity", (float)movementIntensity);
    }

    // Returns movement intensity (0 to 1) based on speed
    public float GetMovementIntensity()
    {
        return Mathf.Clamp01((currentSpeed - normalSpeed) / (sprintSpeed - normalSpeed));
    }
    public void StartSprint()
    {
        if (!animator.GetBool("isCrouching")) // Ensure not crouching
        {
            isSprinting = true;
        }
    }

}
