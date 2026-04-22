using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput input;
    [SerializeField] float speed = 10;
    [SerializeField] float jumpForce = 10;
    [SerializeField] Transform feet;
    [SerializeField] Vector2 onGroundSize;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = rb.GetComponent<PlayerInput>();
    }
    void Update()
    {
        if (input.jumpPressed && Physics2D.OverlapBox(feet.position, onGroundSize, 0, LayerMask.GetMask("Ground")))
        {
            rb.linearVelocityY = jumpForce;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = input.horizontalMove * speed;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(feet.position, onGroundSize);
    }
}
