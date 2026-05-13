using UnityEngine;

public class PlaceBomb : PlaceAbility
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private Rigidbody2D rb;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void Do()
    {
        Bomb newBomb = Instantiate(bomb, transform.position + (Vector3.right * (sr.flipX ? -1 : 1) + Vector3.up),
            Quaternion.identity).GetComponent<Bomb>();
        newBomb.GetComponent<Rigidbody2D>().linearVelocity = rb.linearVelocity;
        GameController.Instance.AddBomb(newBomb);
    }
}
