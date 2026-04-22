using UnityEngine;
using static StatusType;

public class BallSpawnerMovement : MonoBehaviour
{
    private PlayerInputBehaivour playerInput;
    private Rigidbody2D rb;
    private StatusBehaivour status;

    private void Start()
    {
        playerInput = GetComponent<PlayerInputBehaivour>();
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<StatusBehaivour>();
    }

    void Update()
    {
        rb.linearVelocity = playerInput.moveDirection * status[LauncherSpeed];
    }
}
