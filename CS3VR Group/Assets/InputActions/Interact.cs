using UnityEngine;

public class Interact : MonoBehaviour
{
    Transform cam;
    [SerializeField] float interactDistance = 50f;
    private IInteractable lastHoveredObject;  // Keep track of the last hovered object

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    public void InteractWithObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, interactDistance))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void HoverOverObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, interactDistance))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();

            // Check if we're hovering over a new object
            if (interactable != null && interactable != lastHoveredObject)
            {
                // If we were previously hovering over another object, end hover on it
                if (lastHoveredObject != null)
                {
                    lastHoveredObject.OnHoverExit();
                }
                interactable.OnHoverEnter();  // Start hover on new object
                lastHoveredObject = interactable;
            }
        }
        else if (lastHoveredObject != null)
        {
            // No object is being hovered over, so call OnHoverExit on the last object
            lastHoveredObject.OnHoverExit();
            lastHoveredObject = null;
        }
    }
}