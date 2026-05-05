using UnityEngine;

public class TransformAvaragePosition : MonoBehaviour
{
    public Transform[] transforms;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 followPosition = Vector2.zero;
        for (int i = 0; i < transforms.Length; i++)
        {
            followPosition += (Vector2)transforms[i].position;
        }
        followPosition /= transforms.Length;
        followPosition -= (Vector2)transform.position;
        rb.linearVelocity = Mathf.Clamp(speed * speed, minSpeed, maxSpeed) * followPosition;
    }
}
