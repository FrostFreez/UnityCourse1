using UnityEngine;
using UnityEngine.AI;

public class WaypointMovement : CoreComponent
{
    private WaypointManager wm;
    private NavMeshAgent agent;

    public override void StartComponent()
    {
        wm = controller.FindCore<WaypointManager>();
        agent = controller.GetComponent<NavMeshAgent>();
        agent.destination = wm.GetRandomWaypoint().position;
    }
    public void SetWaypoint(Transform newWaypoint)
    {
        agent.destination = newWaypoint.position;
    }
    public void SetWaypoint(Vector3 newWaypoint)
    {
        agent.destination = newWaypoint;
    }
    public Transform SetRandomWaypoint()
    {
        Transform ret = wm.GetRandomWaypoint();
        agent.destination = ret.position;
        return ret;
    }
}
