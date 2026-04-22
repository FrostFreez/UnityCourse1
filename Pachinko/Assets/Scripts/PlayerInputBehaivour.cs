using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputBehaivour : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerInput;

    private InputAction attackAction;
    public bool attackPressed = false;

    private InputAction moveAction;
    public Vector2 moveDirection;

    private void Start()
    {
        attackAction = playerInput.FindAction("Attack");
        moveAction = playerInput.FindAction("Move");
    }

    private void Update()
    {
        attackPressed = attackAction.IsPressed();
        moveDirection = moveAction.ReadValue<Vector2>();
    }
}
