using UnityEngine;

public class HopLedge : CoreComponent
{
    private MovementHandler mh;
    private CollisionDetection cd;

    public override void StartComponent()
    {
        mh = controller.FindComponent<MovementHandler>();
        cd = controller.FindComponent<CollisionDetection>();
    }

    public override void UpdateComponent()
    {
        if (cd.ledgeDetected && !cd.headDetected && cd.feetDetected)
        {
            Vector2 velocity = new(mh.velocity.x, mh.velocity.z);
            if (velocity.sqrMagnitude > 0 )
            {
                controller.transform.position += Vector3.up;
            }
        }
    }
}
