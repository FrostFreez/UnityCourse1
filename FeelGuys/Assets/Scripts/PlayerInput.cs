using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : CoreComponent
{
    [SerializeField] private InputActionAsset actions;
    private InputAction moveAction;
    public Vector2 moveDirection;

    private InputAction lookAction;
    public Vector2 lookRotation;

    private InputAction jumpAction;
    public bool jumpHeld = false;
    public bool jumpPressed = false;
    public bool jumpReleased = false;

    private InputAction useAction;
    public bool useHeld = false;
    public bool usePressed = false;
    public bool useReleased = false;

    public override void StartComponent()
    {
        moveAction = actions.FindActionMap("Player").FindAction("Move");
        lookAction = actions.FindActionMap("Player").FindAction("Look");
        jumpAction = actions.FindActionMap("Player").FindAction("Jump");
        useAction = actions.FindActionMap("Player").FindAction("Use");
    }

    public override void UpdateComponent()
    {
        moveDirection = moveAction.ReadValue<Vector2>();
        lookRotation = lookAction.ReadValue<Vector2>();
        jumpReleased = jumpHeld && !jumpAction.IsPressed();
        jumpPressed = !jumpHeld && jumpAction.IsPressed();
        jumpHeld = jumpAction.IsPressed();
        useReleased = useHeld && !useAction.IsPressed();
        usePressed = !useHeld && useAction.IsPressed();
        useHeld = useAction.IsPressed();
    }
}
