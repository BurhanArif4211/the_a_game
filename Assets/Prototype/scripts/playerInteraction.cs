using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    private bool isInRange = false;
    private Interactable currentInteractable;

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E)) // Press E to interact
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact(); // Call the Interact method
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entered the trigger is an interactable object
        if (other.CompareTag("interaction"))
        {
            Debug.Log("Player is in range to interact with: " + other.name);
            currentInteractable = other.GetComponent<Interactable>(); // Get the interactable script
            isInRange = true; // Player is in range to interact
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When the player leaves the trigger area
        if (other.CompareTag("interaction"))
        {
            currentInteractable = null; // No longer interactable
            isInRange = false; // Player is out of range
        }
    }
}
