using UnityEngine;

public class FaceDirection : CoreComponent
{
    [SerializeField] private Transform mesh;
    [SerializeField] private BaseMovementHandler mh;
    [SerializeField] private float speed;

    public override void StartComponent()
    {
        mh = controller.FindCore<BaseMovementHandler>();
        mesh = controller.FindCore<MeshController>().mesh;
    }
    public override void UpdateComponent()
    {
        if (Mathf.Abs(mh.velocity.x) > 0.2f | Mathf.Abs(mh.velocity.z) > 0.2f)
        {
            float current = mesh.transform.eulerAngles.y;
            float target = Mathf.Atan2(mh.velocity.x, mh.velocity.z) * Mathf.Rad2Deg;
            target = target < 0 ? target + 360 : target;

            float next = 0;
            if (current == target) return;
            else if (Mathf.Abs(target - current) < 180 && Mathf.Abs(target - current) > -180)
                next = current + (target - current) * speed * Time.deltaTime;
            else
                next = current + (current - target) * speed * Time.deltaTime;

            mesh.transform.rotation = Quaternion.Euler(0, next, 0);
        }
    }
}
