using UnityEngine;

public class FenceBoost : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float normalJumpForce = 15f;
    [SerializeField] private float boostedJumpForce = 22f;
    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Collider2D ground = Physics2D.OverlapBox(player.feet.position, player.feetSize, 0, LayerMask.GetMask("Ground"));
        if (ground != null)
        {
            if (ground.name.Contains("Fences") | ground.name == "Breakable")
            {
                player.jumpForce = boostedJumpForce;
            }
            else
            {
                player.jumpForce = normalJumpForce;
            }
        }
    }
}
