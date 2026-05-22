using UnityEngine;

public class RespawnEntity : Killable
{
    [SerializeField] private CapsuleCollider mesh;
    [SerializeField] private MovementHandler mh;
    [SerializeField] private Vector3 bodySize;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Vector3 respawnForce;

    public override void StartComponent()
    {
        mesh = controller.FindComponent<MeshController>().GetComponent<CapsuleCollider>();
        mh = controller.FindComponent<MovementHandler>();
        spawnPoint = transform.position;
        bodySize.x = mesh.radius;
        bodySize.y = mesh.height;
        bodySize.z = mesh.radius;
    }
    public override void UpdateComponent()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, bodySize, Quaternion.identity, LayerMask.GetMask("Portal"));
        if (colliders.Length > 0)
        {
            spawnPoint = colliders[0].transform.parent.GetChild(0).position;
        }
    }

    public override void Kill()
    {
        controller.transform.position = spawnPoint;
        mh.ReplaceForce(respawnForce);
    }
}
