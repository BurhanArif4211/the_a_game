using UnityEngine;

public class Interactable : MonoBehaviour
{
    // This method can be overridden in different interactables for custom behavior
    public virtual void Interact()
    {
        Debug.Log("Interacted with: " + gameObject.name);
    }
}
