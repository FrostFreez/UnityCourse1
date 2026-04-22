using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * speed;
    }

    public void Shot(Vector2 direction)
    {
        this.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.attachedRigidbody.linearVelocity = 2 * speed * (direction + Vector2.up).normalized;
        }
    }
}
