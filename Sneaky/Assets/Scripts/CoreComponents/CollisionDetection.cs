using UnityEngine;
using System.Collections.Generic;
using System;

public class CollisionDetection : CoreComponent
{
    private Transform direction;

    [SerializeField] private Dictionary<string, Detection> checks = new();

    public override void StartComponent()
    {
        direction = controller.FindCore<MeshController>().mesh;
    }
    public override void UpdateComponent()
    {
        foreach (var v in checks)
        {
            v.Value.Detect();
        }
    }
    private void OnValidate()
    {
        if (checks.Count == 0)
        {
            checks.Add(
                    "Ground",
                    new(transform, Vector3.one, 0)
                );
        }
    }

    public bool GetCollision(string collisionName)
    {
        Detection collision = checks[collisionName];
        if (collision != null)
        {
            return collision.detected;
        }
        else
        {
            Debug.Log(controller.name + ": Is trying to access inexistent collision detection " + collisionName);
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var v in checks)
        {
            v.Value.DrawGizmos();
        }
    }
}

[Serializable]
public class Detection
{
    [SerializeField] private Transform location;
    [SerializeField] private Vector3 size;
    [SerializeField] private LayerMask layerCheck;
    [SerializeField] public bool detected = false;

    public Detection(Transform location, Vector3 size, LayerMask layerCheck)
    {
        this.location = location;
        this.size = size;
        this.layerCheck = layerCheck;
    }

    public void Detect()
    {
        if (location)
        {
            detected = Physics.OverlapBox(location.position, size * 0.5f, Quaternion.identity, layerCheck).Length > 0;
        }
    }
    public void DrawGizmos()
    {
        if (location)
        {
            Gizmos.DrawCube(location.position, size * 0.5f);
        }
    }
}