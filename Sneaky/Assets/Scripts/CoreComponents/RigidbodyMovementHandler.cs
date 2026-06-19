using UnityEngine;

public class RigidbodyMovementHandler : BaseMovementHandler
{
    private Rigidbody rb;
    [SerializeField] private float damping;
    public override void StartComponent()
    {
        rb = controller.GetComponent<Rigidbody>();
        rb.linearDamping = damping;
    }
    public override void UpdateComponent()
    {
         velocity = rb.linearVelocity;
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
