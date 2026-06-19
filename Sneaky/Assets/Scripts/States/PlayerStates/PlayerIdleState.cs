using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public override void UpdateState()
    {
        base.UpdateState();
        if (pi.vector.x != 0 | pi.vector.y != 0)
        {
            stateMachine.ChangeState("moveState");
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        mh.SetVelocityHorizontal(Vector3.zero);
    }
}