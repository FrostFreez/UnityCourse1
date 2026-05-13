using UnityEngine;

public class PlayerController : EntityController
{
    public PlayerInput input;
    
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerAirState airState;

    public Transform feet;
    public Vector2 feetSize;
    public Transform head;

    public float speed;
    public float jumpForce;

    public void Awake()
    {
        idleState = new(this, sm, "idle", this);
        moveState = new(this, sm, "move", this);
        airState = new(this, sm, "air", this);
        sm.Initialize(idleState);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(feet.position, feetSize);
        Gizmos.DrawCube(head.position, feetSize);
    }
}
