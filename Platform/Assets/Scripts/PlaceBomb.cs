using UnityEngine;

public class PlaceBomb : PlaceAbility
{
    [SerializeField] private GameObject bomb;
    protected override void Do()
    {
        Bomb newBomb = Instantiate(bomb, transform.position + (Vector3.right * (sr.flipX ? -1 : 1)),
            Quaternion.identity).GetComponent<Bomb>();
        GameController.Instance.AddBomb(newBomb);
    }
}
