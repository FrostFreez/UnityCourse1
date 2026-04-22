using UnityEngine;

[CreateAssetMenu(fileName = "Charger", menuName = "Collectable/Charger")]
public class Charger : Collectable
{
    public override void Collect(Ball ball)
    {
        ball.status[StatusType.Charges]++;
    }
}