using UnityEngine;

public class PlayerDashState : BaseState
{
    protected MeshController mc;
    protected RigidbodyMovementHandler mh;
    protected InputVector2 pi;
    protected Vector2 direction;
    public float dashDuration = 0.3f;

    public override void SetUp(EntityController controller, StateMachine stateMachine)
    {
        base.SetUp(controller, stateMachine);
        mc = controller.FindCore<MeshController>();
        mh = controller.FindCore<RigidbodyMovementHandler>();
        pi = controller.FindCore<PlayerInput>().GetInputItem<InputVector2>("move");
    }
    public override void Enter()
    {
        base.Enter();
        direction = new(pi.vector.x, pi.vector.y);
    }
    public override void UpdateState()

    {
        base.UpdateState();
        if (Time.time - enterTime > dashDuration)
        {
            stateMachine.ChangeState("idleState");
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        mh.SetVelocityHorizontal((direction.y * mc.mesh.forward + direction.x * mc.mesh.right) * 20);
    }
}
