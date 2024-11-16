using UnityEngine;

public class InputManager : MonoBehaviour
{
    #pragma warning disable 649

    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] Interact interact;
    PCControls controls;
    PCControls.GroundMovementActions groundMovement;
    Vector2 horizontalInput;
    Vector2 mouseInput;

    public void Awake()
    {
        controls = new PCControls();
        groundMovement = controls.GroundMovement;

        // Set up input events
        groundMovement.HorizontalMovement.performed += context => horizontalInput = context.ReadValue<Vector2>();
        groundMovement.MouseX.performed += context => mouseInput.x = context.ReadValue<float>();
        groundMovement.MouseY.performed += context => mouseInput.y = context.ReadValue<float>();
        groundMovement.Interact.performed += _ => interact.InteractWithObject();
    }

    private void Update()
    {
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);
        interact.HoverOverObject();  // Added hover functionality to update
    }

    public void OnEnable()
    {
        groundMovement.Enable();
    }

    public void OnDestroy()
    {
        groundMovement.Disable();
    }
}