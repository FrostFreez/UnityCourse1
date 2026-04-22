using UnityEngine;

[CreateAssetMenu(fileName = "BallSO", menuName = "BallSO/Starter")]
public class StartedBallSO : BallSO
{
    public override void Pressed(Ball ball)
    {

    }
    public override void Released(Ball ball)
    {
        if (ball.status[StatusType.Charges] > 0)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(ball.transform.position, 10f, LayerMask.GetMask("Wall"));

            if (colliders.Length > 0)
            {
                Collider2D closest = colliders[0];
                float closestDistance = (Physics2D.ClosestPoint(ball.transform.position, closest) - (Vector2)ball.transform.position).magnitude;
                for (int i = 1; i < colliders.Length; i++)
                {
                    float distance = (Physics2D.ClosestPoint(ball.transform.position, colliders[i]) - (Vector2)ball.transform.position).magnitude;
                    if (closestDistance > distance)
                    {
                        closestDistance = distance;
                        closest = colliders[i];
                    }
                }

                Vector2 closestPoint = Physics2D.ClosestPoint(ball.transform.position, closest);

                Vector2 direction = (Vector2)ball.transform.position - closestPoint;

                Vector2 rotatedDirection = new(direction.y, -direction.x);

                if (rotatedDirection.y < 0)
                {
                    ball.ApplyForce(rotatedDirection * ball.rb.linearVelocity.magnitude);
                }
                else
                {
                    ball.ApplyForce(-rotatedDirection * ball.rb.linearVelocity.magnitude);
                }
            }
            ball.status[StatusType.Charges]--;
        }
    }
}
