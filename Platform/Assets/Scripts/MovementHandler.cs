using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Vector2 maxMoveSpeed; 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetVelocity(Vector2 newVelocity)
    {
        if (newVelocity.sqrMagnitude < rb.linearVelocity.sqrMagnitude) return;
        rb.linearVelocity = newVelocity;
    }
    public void SetVelocityX(float x)
    {
        if (maxMoveSpeed.x < Mathf.Abs(rb.linearVelocity.x)) return;
        rb.linearVelocityX = x;
    }
    public void SetVelocityY(float y)
    {
        if (maxMoveSpeed.y < Mathf.Abs(rb.linearVelocity.y)) return;
        rb.linearVelocityY = y;
    }
    public void AddForce(Vector2 addedForce)
    {
        rb.linearVelocity += addedForce;
    }
}
