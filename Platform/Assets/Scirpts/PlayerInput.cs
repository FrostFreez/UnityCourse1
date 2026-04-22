using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    private InputAction horizontalMoveAction;
    public float horizontalMove;

    private InputAction jumpAction;
    public bool jumpHeld = false;
    public bool jumpPressed = false;

    private InputAction attackAction;
    public bool attackHeld = false;
    public bool attackPressed = false;

    private void Start()
    {
        horizontalMoveAction = actions.FindAction("Move");
        jumpAction = actions.FindAction("Jump");
        attackAction = actions.FindAction("Attack");
    }

    private void Update()
    {
        horizontalMove = horizontalMoveAction.ReadValue<Vector2>().x;
        jumpPressed = jumpHeld ? false : jumpAction.IsPressed();
        jumpHeld = jumpAction.IsPressed();
        attackPressed = attackHeld ? false : attackAction.IsPressed();
        attackHeld = attackAction.IsPressed();
    }

}
