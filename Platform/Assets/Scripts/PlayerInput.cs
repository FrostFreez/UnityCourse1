using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private int playerIndex;
    private InputAction moveAction;
    public Vector2 moveDirection;

    private InputAction jumpAction;
    public bool jumpHeld = false;
    public bool jumpPressed = false;
    public bool jumpReleased = false;

    private InputAction useAction;
    public bool useHeld = false;
    public bool usePressed = false;
    public bool useReleased = false;

    public void Start()
    {
        moveAction = actions.FindActionMap("Player" + playerIndex).FindAction("Move");
        jumpAction = actions.FindActionMap("Player" + playerIndex).FindAction("Jump");
        useAction = actions.FindActionMap("Player" + playerIndex).FindAction("Use");
    }

    public void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();
        jumpReleased = jumpHeld && !jumpAction.IsPressed();
        jumpPressed = !jumpHeld && jumpAction.IsPressed();
        jumpHeld = jumpAction.IsPressed();
        useReleased = useHeld && !useAction.IsPressed();
        usePressed = !useHeld && useAction.IsPressed();
        useHeld = useAction.IsPressed();
    }
}
