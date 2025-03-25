using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Target : MonoBehaviour
{
    public Transform targetTransform;
    public float speed = 14f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        Vector3 displacementFromTarget = targetTransform.position - transform.position;
        Vector3 directionToTarget = displacementFromTarget.normalized;
        Vector3 velocity = directionToTarget * speed;
        Vector3 moveAmount = velocity * Time.deltaTime;
        float distanceToTarget = displacementFromTarget.magnitude;
        if (distanceToTarget > 1.9f)
        {
            transform.position += moveAmount;
            // transform.Translate(velocity * Time.deltaTime);
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
