using UnityEngine;
using UnityEngine.Tilemaps;
public class Bomb : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float cooldownDuration = 5f;
    [SerializeField] private float explosionForce = 20f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private bool exploded = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        cooldownDuration -= Time.deltaTime;
        if (cooldownDuration < 0 && !exploded)
        {
            exploded = true;
            anim.speed = 1;
            anim.SetTrigger("Exploded");
            rb.freezeRotation = true;
            transform.rotation = Quaternion.identity;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (Collider2D collider in colliders)
            {
                switch (collider.tag)
                {
                    case "Player":
                        collider.GetComponent<MovementHandler>().AddForce(
                            (collider.transform.position - transform.position).normalized * explosionForce);
                        break;
                    case "Fence":
                        Tilemap tilemap = collider.GetComponent<Tilemap>();
                        for (int i = -(int)explosionRadius; i <= explosionRadius; i++)
                        {
                            for (int j = -(int)explosionRadius; j <= explosionRadius; j++)
                            {
                                Vector3Int pos = tilemap.layoutGrid.WorldToCell(transform.position + new Vector3(i, j));
                                tilemap.SetTile(pos, null);
                            }
                        }
                        break;
                    default: break;
                }
            }
        }
        else if (!exploded)
        {
            anim.speed = 1f / cooldownDuration;
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
