using System;
using UnityEngine;

public abstract class PlayerState : BaseState
{
    public PlayerState(EntityController controller, StateMachine stateMachine, string animString, PlayerController player) : base(controller, stateMachine, animString)
    {
        this.player = player;
    }
    [field: HideInInspector] protected PlayerController player;
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
        isGrounded = Physics2D.OverlapBox(player.feet.position, player.feetSize, 0, LayerMask.GetMask("Ground"));
        canJump = !Physics2D.OverlapBox(player.head.position, player.feetSize, 0, LayerMask.GetMask("Player"));
        isJumping = player.input.jumpHeld;
        input = player.input.moveDirection;
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
            player.mh.SetVelocityY(player.jumpForce);
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
        if (input.x != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.mh.SetVelocityX(0);
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
        if (input.x == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (input.x > 0)
        {
            player.sr.flipX = false;
        }
        else if (input.x < 0)
        {
            player.sr.flipX = true;
        }
    }
    public override void PhysicsUpdate()
    {
        player.mh.SetVelocityX(input.x * player.speed);
        base.PhysicsUpdate();
    }
}

[Serializable]
public class PlayerAirState : PlayerState
{
    [field: SerializeField] protected bool IsGrounded;
    [field: SerializeField] protected bool isJumping;
    [field: SerializeField] protected bool jumped;
    [field: SerializeField] protected Vector2 input;
    public PlayerAirState(EntityController controller, StateMachine stateMachine, string animString, PlayerController player) : base(controller, stateMachine, animString, player)
    {
    }
    public override void Enter()
    {
        base.Enter();

        jumped = player.input.jumpHeld;
    }
    public override void DoChecks()
    {
        IsGrounded = Physics2D.OverlapBox(player.feet.position, player.feetSize, 0, LayerMask.GetMask("Ground"));
        isJumping = player.input.jumpHeld;
        input = player.input.moveDirection;
        base.DoChecks();
    }
    public override void Update()
    {
        base.Update();
        if (IsGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (input.x > 0)
        {
            player.sr.flipX = false;
        }
        else if (input.x < 0)
        {
            player.sr.flipX = true;
        }

        if (player.input.jumpReleased && player.rb.linearVelocityY > 0)
        {
            player.mh.SetVelocityY(0);
        }else if (player.input.jumpPressed && Time.time - enterTime < 0.02f && !jumped)
        {
            player.mh.SetVelocityY(player.jumpForce);
            jumped = true;
        }
        controller.anim.SetFloat("YVelocity", controller.rb.linearVelocityY);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.mh.SetVelocityX(input.x * player.speed);
    }
}