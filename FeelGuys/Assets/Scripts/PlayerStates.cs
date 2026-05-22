using System;
using UnityEngine;
using Photon.Pun;

public abstract class PlayerState : BaseState
{
    [field: SerializeField] protected MovementHandler mh;
    [field: HideInInspector] protected PlayerController player;
    [field: SerializeField] protected PlayerInput playerInput;
    [field: SerializeField] protected CollisionDetection playerCollision;
    [field: SerializeField] protected Transform direction;
    public PlayerState(EntityController controller, StateMachine stateMachine, string animString, PlayerController player) : base(controller, stateMachine, animString)
    {
        if (controller.pv.IsMine)
        {
            this.player = player;
            playerInput = player.FindComponent<PlayerInput>();
            playerCollision = player.FindComponent<CollisionDetection>();
            mh = player.FindComponent<MovementHandler>();
            direction = player.FindComponent<CameraController>().transform;
        }
    }
}

public abstract class PlayerGroundedState : PlayerState
{
    [field: SerializeField] protected bool isGrounded;
    [field: SerializeField] protected bool isJumping;
    [field: SerializeField] protected bool canJump;
    [field: SerializeField] protected Vector2 input;
    public PlayerGroundedState(EntityController controller, StateMachine stateMachine, string animString, PlayerController player) : base(controller, stateMachine, animString, player)
    {
    }
    public override void DoChecks()
    {
        isGrounded = playerCollision.feetDetected;
        canJump = !playerCollision.headDetected;
        isJumping = playerInput.jumpHeld;
        input = playerInput.moveDirection;
        base.DoChecks();
    }
    public override void Update()
    {
        base.Update();
        if (!isGrounded)
        {
            stateMachine.ChangeState(player.airState);
        }
        if (isJumping && canJump)
        {
            mh.SetVelocityY(player.jumpForce);
        }
    }
}

[Serializable]
public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(EntityController controller, StateMachine stateMachine, string animString, PlayerController player) : base(controller, stateMachine, animString, player)
    {
    }
    public override void Update()
    {
        base.Update();
        if (input.x != 0 | input.y != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        mh.SetVelocityHorizontal(Vector3.zero);
    }
}

[Serializable]
public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(EntityController controller, StateMachine stateMachine, string animString, PlayerController player) : base(controller, stateMachine, animString, player)
    {
    }
    public override void Update()
    {
        base.Update();
        if (input.x == 0 & input.y == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
    public override void PhysicsUpdate()
    {
        mh.SetVelocityHorizontal((input.y * direction.forward + input.x * direction.right) * player.speed);
        base.PhysicsUpdate();
    }
}

[Serializable]
public class PlayerAirState : PlayerState
{
    [field: SerializeField] protected bool isGrounded;
    [field: SerializeField] protected bool isJumping;
    [field: SerializeField] protected bool jumped;
    [field: SerializeField] protected Vector2 input;
    public PlayerAirState(EntityController controller, StateMachine stateMachine, string animString, PlayerController player) : base(controller, stateMachine, animString, player)
    {
    }
    public override void Enter()
    {
        base.Enter();

        jumped = playerInput.jumpHeld;
    }
    public override void DoChecks()
    {
        isGrounded = playerCollision.feetDetected;
        isJumping = playerInput.jumpHeld;
        input = playerInput.moveDirection;
        base.DoChecks();
    }
    public override void Update()
    {
        base.Update();

        player.anim.SetFloat("YVelocity", mh.velocity.y);

        if (isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (playerInput.jumpReleased && mh.velocity.y > 0)
        {
            mh.SetVelocityY(0);
        }
        else if (playerInput.jumpPressed && Time.time - enterTime < 0.02f && !jumped)
        {
            mh.SetVelocityY(player.jumpForce);
            jumped = true;
        }
        else if (playerInput.jumpPressed)
        {
            stateMachine.ChangeState(player.diveState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        mh.SetVelocityHorizontal((input.y * direction.forward + input.x * direction.right) * player.speed);
    }
}

[Serializable]
public class PlayerDiveState : PlayerState
{
    [field: SerializeField] private bool isGrounded = false;
    public PlayerDiveState(EntityController controller, StateMachine stateMachine, string animString, PlayerController player) : base(controller, stateMachine, animString, player)
    {
    }
    public override void Enter()
    {
        base.Enter();

        mh.AddForce(mh.velocity.normalized * 5 + Vector3.up * 4);
    }
    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = playerCollision.feetDetected;
    }
    public override void Update()
    {
        base.Update();

        if (isGrounded)
        {
            stateMachine.ChangeState(player.landState);
        }
    }
}

[Serializable]
public class PlayerLandState : PlayerState
{
    public PlayerLandState(EntityController controller, StateMachine stateMachine, string animString, PlayerController player) : base(controller, stateMachine, animString, player)
    {
    }
    public override void Update()
    {
        base.Update();
        if (mh.velocity.sqrMagnitude < 9)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}