using UnityEngine;

public class MovementHandler : CoreComponent
{
    private Rigidbody rb;
    private CollisionDetection cd;
    [SerializeField] public Vector3 velocity;
    [SerializeField] public float dampingOnGround;
    [SerializeField] public float dampingOffGround;
    public override void StartComponent()
    {
        rb = controller.GetComponent<Rigidbody>();
        cd = controller.FindComponent<CollisionDetection>();
    }
    public override void UpdateComponent()
    {
         velocity = rb.linearVelocity;
        if (cd.feetDetected)
        {
            rb.linearDamping = dampingOnGround;
        }
        else
        {
            rb.linearDamping = dampingOffGround;
        }
    }
    public void SetVelocityHorizontal(Vector3 newVelocityXZ)
    { 
        rb.linearVelocity = new Vector3(newVelocityXZ.x, rb.linearVelocity.y, newVelocityXZ.z);
    }
    public void SetVelocityY(float y)
    {
        if (10 < Mathf.Abs(rb.linearVelocity.y)) return;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, y, rb.linearVelocity.z);
    }
    public void ReplaceForce(Vector3 newForce)
    {
        rb.linearVelocity = newForce;
    }
    public void AddForce(Vector3 adddForce)
    {
        rb.linearVelocity += adddForce;
    }
}
