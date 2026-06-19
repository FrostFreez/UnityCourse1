using UnityEngine;

public class RoamingState : BaseState
{
    private WaypointMovement wm;
    private Collider player;
    public Transform mesh;
    public float duration = 3;
    public float maxView = 50;
    public override void SetUp(EntityController controller, StateMachine stateMachine)
    {
        base.SetUp(controller, stateMachine);
        mesh = controller.FindCore<MeshController>().mesh;
        wm = controller.FindCore<WaypointMovement>();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        Collider[] colliders = { null };
        if (Physics.OverlapSphereNonAlloc(transform.position, maxView, colliders, LayerMask.GetMask("Player")) > 0)
        {
            player = colliders[0];
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
        if (duration < Time.time - enterTime)
        {
            stateMachine.ChangeState("WaypointState");
        }
        else
        {
            wm.SetWaypoint(player.transform);
        }
    }
}
