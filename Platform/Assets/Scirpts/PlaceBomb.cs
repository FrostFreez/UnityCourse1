using UnityEngine;

public class PlaceBomb : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private GameObject bomb;
    [SerializeField] private SpriteRenderer sr;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (input.usePressed)
        {
            Instantiate(bomb, transform.position + (Vector3.right * (sr.flipX ? -1 : 1)), Quaternion.identity);
        }
    }
}
