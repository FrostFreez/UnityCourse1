using System;
using UnityEngine;

public abstract class PlayerGroundedState : BaseState
{
    protected MeshController mc;
    protected RigidbodyMovementHandler mh;
    protected InputVector2 pi;
    protected InputButton dashInput;

    public override void SetUp(EntityController controller, StateMachine stateMachine)
    {
        base.SetUp(controller, stateMachine);
        mc = controller.FindCore<MeshController>();
        mh = controller.FindCore<RigidbodyMovementHandler>();
        pi = controller.FindCore<PlayerInput>().GetInputItem<InputVector2>("move");
        dashInput = controller.FindCore<PlayerInput>().GetInputItem<InputButton>("dash");
    }

    private void Update()
    {
        if (dashInput.pressed)
        {
            stateMachine.ChangeState("dashState");
        }
    }
}