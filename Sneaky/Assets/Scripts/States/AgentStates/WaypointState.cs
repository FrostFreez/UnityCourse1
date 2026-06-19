using UnityEngine;

public class WaypointState : BaseState
{
    [SerializeField] private Transform mesh;
    [SerializeField] private WaypointMovement wm;
    [SerializeField] private Transform currentDestiny;
    public float maxView = 50;
    public override void SetUp(EntityController controller, StateMachine stateMachine)
    {
        base.SetUp(controller, stateMachine);
        wm = controller.FindCore<WaypointMovement>();
        mesh = controller.FindCore<MeshController>().mesh;
    }
    public override void Enter()
    {
        base.Enter();
        currentDestiny = wm.SetRandomWaypoint();
    }
    public override void DoChecks()
    {
        base.DoChecks();
        Collider[] colliders = { null };
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
        if (Vector3.SqrMagnitude(currentDestiny.position - transform.position) < 1.5f)
        {
            stateMachine.ChangeState("RotateState");
        }
    }
}