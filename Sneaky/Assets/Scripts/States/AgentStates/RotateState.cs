using UnityEngine;

public class RotateState : BaseState
{
    Transform mesh;
    public float angle = 0;
    public float duration = 3;
    public float maxView = 50;
    public override void SetUp(EntityController controller, StateMachine stateMachine)
    {
        base.SetUp(controller, stateMachine);
        mesh = controller.FindCore<MeshController>().mesh;
    }
    public override void Enter()
    {
        base.Enter();
        angle = mesh.eulerAngles.y;
    }
    public override void DoChecks()
    {
        base.DoChecks();
        Collider[] colliders = { null };
        if (duration < Time.time - enterTime)
        {
            stateMachine.ChangeState("WaypointState");
        }
        if (Physics.OverlapSphereNonAlloc(transform.position, maxView, colliders, LayerMask.GetMask("Player")) > 0)
        {
            Collider player = colliders[0];
            Vector3 direction = player.transform.position - transform.position;
            float viewAngle = Vector3.Angle(mesh.forward, direction);
            if (Mathf.Abs(viewAngle) < 25)
            {
                RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, maxView, LayerMask.GetMask("Player", "Ground"));
                float minDist = float.MaxValue;
                int minDistIndex = 0;
                for (int i = 0; i < hits.Length; i++)
                {
                    float dist = Vector3.SqrMagnitude(transform.position - hits[i].point);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        minDistIndex = i;
                    }
                }
                if (minDist < float.MaxValue)
                {
                    if (hits[minDistIndex].collider == player)
                    {
                        stateMachine.ChangeState("ChaseState");
                    }
                }
            }
        }

    }
    public override void UpdateState()
    {
        base.UpdateState();
        angle += Time.deltaTime * 360 / duration;
        mesh.eulerAngles = new(mesh.eulerAngles.x, angle, mesh.eulerAngles.z);
    }
    public override void Exit()
    {
        base.Exit();
        controller.transform.eulerAngles = new(mesh.eulerAngles.x, angle, mesh.eulerAngles.z);
        mesh.rotation = controller.transform.rotation;
    }
}
