using UnityEngine;

public class ChaseState : BaseState
{
    public WaypointMovement wm;
    public Collider target;
    public float maxChaseTime = 1;
    public float chasingTime = 0;
    public float maxView = 50;
    public override void SetUp(EntityController controller, StateMachine stateMachine)
    {
        base.SetUp(controller, stateMachine);
        wm = controller.FindCore<WaypointMovement>();
    }
    public override void Enter()
    {
        base.Enter();
        Collider[] colliders = { null };
        Debug.Log("Enter");
        if (Physics.OverlapSphereNonAlloc(transform.position, maxView, colliders, LayerMask.GetMask("Player")) > 0)
        {
            target = colliders[0];
            wm.SetWaypoint(target.transform);
        }
    }
    public override void UpdateState()
    {
        base.UpdateState();
        RaycastHit[] hits = Physics.RaycastAll(transform.position, target.transform.position - transform.position, maxView, LayerMask.GetMask("Player", "Ground"));
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
            if (hits[minDistIndex].collider == target)
            {
                wm.SetWaypoint(target.transform);
            }
            else
            {
                stateMachine.ChangeState("RoamingState");
            }
        }
        else
        {
            stateMachine.ChangeState("RoamingState");
        }
    }
}