public class PlayerMoveState : PlayerGroundedState
{
    public override void UpdateState()
    {
        base.UpdateState();
        if (pi.vector.x == 0 & pi.vector.y == 0)
        {
            stateMachine.ChangeState("idleState");
        }
    }
    public override void PhysicsUpdate()
    {
        mh.SetVelocityHorizontal((pi.vector.y * mc.mesh.forward + pi.vector.x * mc.mesh.right) * 5);
        base.PhysicsUpdate();
    }
}