using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : CoreComponent
{
    [SerializeField] private List<Transform> waypoints;

    public Transform GetIndexedWaypoint(int index)
    {
        if (waypoints.Count == 0)
        {
            Debug.LogWarning(name + ": No waypoint found");
            return null;
        }
        if (waypoints.Count <= index)
        {
            Debug.LogWarning(name + ": Index out of bounds");
            return null;
        }
        return waypoints[index];
    }

    public Transform GetRandomWaypoint()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogWarning(name + ": No waypoitn found");
            return null;
        }
        return waypoints[Random.Range(0, waypoints.Count)];
    }
    public Transform GetClosestWaypoint()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogWarning(name + ": No waypoitn found");
            return null;
        }

        float sqrDistance = float.MaxValue;
        int index = 0;

        for (int i = 0; i < waypoints.Count; i++)
        {
            float thisSqrDistace = Vector3.SqrMagnitude(waypoints[i].position - transform.position);
            if (thisSqrDistace < sqrDistance)
            {
                sqrDistance = thisSqrDistace;
                index = i;
            }
        }
        return waypoints[index];
    }
}
