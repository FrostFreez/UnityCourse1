using UnityEngine;
using static StatusType;

public class TrajectoryLine : MonoBehaviour
{
    public int segmentCount = 50;
    public float lineSize = 3;
    private Vector2[] segments = { };
    private LineRenderer lineRenderer;

    [SerializeField] private BallSpawner spawner;
    [SerializeField] private PlayerInputBehaivour input;
    private void Start()
    {
        segments = new Vector2[segmentCount];

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount;
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0;

        spawner = GetComponentInParent<BallSpawner>();
        input = GetComponentInParent<PlayerInputBehaivour>();
    }

    private void Update()
    {
        if (spawner.ready)
        {
            segments[0] = transform.position;
            lineRenderer.SetPosition(0, transform.position);

            Vector2 velocity = spawner.totalForce;

            for (int i = 1; i < segmentCount; i++)
            {
                float timeOffset = i * Time.fixedDeltaTime * lineSize;
                Vector2 gravityOffset = 0.5f * spawner.nextBall[Gravity] * Mathf.Pow(timeOffset, 2) * Physics2D.gravity;

                segments[i] = segments[0] + velocity * timeOffset + gravityOffset;
                lineRenderer.SetPosition(i, segments[i]);
            }
            lineRenderer.startWidth = 0.5f;
        }
        else
        {
            lineRenderer.startWidth = 0;
        }
    }
}
