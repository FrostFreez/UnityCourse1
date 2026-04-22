using UnityEngine;

[CreateAssetMenu(fileName = "ExtraLife", menuName = "Collectable/ExtraLife")]
public class ExtraLife : Collectable
{
    public override void Collect(Ball ball)
    {
        ball.status.baseStatus.Find(x => x.type == StatusType.HP).value++;
    }
}