using UnityEngine;

public class CollisionDetection : CoreComponent
{
    private Transform direction;

    [SerializeField] private Transform feet;
    [SerializeField] private Vector3 feetSize;
    [SerializeField] private LayerMask feetLayerCheck;
    [SerializeField] public bool feetDetected = false;

    [SerializeField] private Transform head;
    [SerializeField] private Vector3 headSize;
    [SerializeField] private LayerMask headLayerCheck;
    [SerializeField] public bool headDetected;

    [SerializeField] private Transform ledge;
    [SerializeField] private Vector3 ledgeSize;
    [SerializeField] private LayerMask ledgeLayerCheck;
    [SerializeField] public bool ledgeDetected;

    [SerializeField] private Transform wall;
    [SerializeField] private Vector3 walllSize;
    [SerializeField] private LayerMask wallLayerCheck;
    [SerializeField] public bool wallDetected;

    public override void StartComponent()
    {
        direction = controller.FindComponent<MeshController>().mesh;
    }
    public override void UpdateComponent()
    {
        if (feet)
        {
            feetDetected = Physics.OverlapBox(feet.position, feetSize * 0.5f, Quaternion.identity, feetLayerCheck).Length > 0;
        }
        if (head)
        {
            headDetected = Physics.OverlapBox(head.position, headSize * 0.5f, Quaternion.identity, headLayerCheck).Length > 0;
        }
        Vector3 frontDirection = direction.forward * 0.1f;
        frontDirection.y = 0;
        frontDirection += transform.position;

        Vector3 ledgeDirection = frontDirection;
        ledgeDirection.y += 0.2f;
        ledge.position = ledgeDirection;
        if (ledge)
        {
            ledgeDetected = Physics.OverlapBox(ledge.position, ledgeSize * 0.5f, Quaternion.identity, ledgeLayerCheck).Length > 0;
        }

        Vector3 wallDirection = frontDirection;
        wallDirection.y += 1.2f;
        wall.position = wallDirection;
        if (wall)
        {
            wallDetected = Physics.OverlapBox(wall.position, walllSize * 0.5f, Quaternion.identity, wallLayerCheck).Length > 0;
            if (ledge) ledgeDetected = !wallDetected && ledgeDetected;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (feet)
        {
            Gizmos.DrawCube(feet.position, feetSize);
        }
        Gizmos.color = Color.red;
        if (head)
        {
            Gizmos.DrawCube(head.position, headSize);
        }
        Gizmos.color = Color.blue;
        if (ledge)
        {
            Gizmos.DrawCube(ledge.position, ledgeSize);
        }
        Gizmos.color = Color.green;
        if (wall)
        {
            Gizmos.DrawCube(wall.position, walllSize);
        }
    }
}
