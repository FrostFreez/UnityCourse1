using UnityEngine;

public class PlayerController : EntityController
{
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerAirState airState;
    public PlayerDiveState diveState;
    public PlayerLandState landState;

    public float speed;
    public float jumpForce;

    public override void Start()
    {
        base.Start();
        if (pv.IsMine)
        {
            idleState = new(this, sm, "idle", this);
            moveState = new(this, sm, "move", this);
            airState = new(this, sm, "air", this);
            diveState = new(this, sm, "dive", this);
            landState = new(this, sm, "dive", this);
            sm.Initialize(idleState);
        }
    }
}
