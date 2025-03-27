using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovementAdvance movement;

    [Header("Speed Settings")]
    [SerializeField] private float normalSpeed = 3f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float acceleration = 5f; // Controls smooth sprinting

    private float currentSpeed; // Tracks player speed for smooth transition

    void Start()
    {
        currentSpeed = normalSpeed; // Initialize speed
    }

    void Update()
    {
        HandleMovement();
        UpdateAnimator();
    }

    /// <summary>
    /// Handles movement and speed transitions.
    /// </summary>
    private void HandleMovement()
    {
        // Determine target speed based on sprint input
        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : normalSpeed;

        // Smooth transition between speeds
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);

        // Apply the movement speed
        movement.SetSpeed(currentSpeed);
    }

    /// <summary>
    /// Updates all animation states in one place.
    /// </summary>
    private void UpdateAnimator()
    {
        float movementIntensity = GetMovementIntensity(); // Get normalized speed value

        animator.SetBool("isWalking", movementIntensity > 0);
        animator.SetBool("isSprinting", movementIntensity > 0.6f); // Adjust threshold if needed
        animator.SetFloat("playerVelocity", movementIntensity); // Blends animations
    }

    /// <summary>
    /// Returns a value between 0 (idle) and 1 (full sprint).
    /// </summary>
    public float GetMovementIntensity()
    {
        return Mathf.Clamp01((currentSpeed - normalSpeed) / (sprintSpeed - normalSpeed));
    }
}
