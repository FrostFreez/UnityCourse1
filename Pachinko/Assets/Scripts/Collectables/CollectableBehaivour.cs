using UnityEngine;

public class CollectableBehaivour : MonoBehaviour
{
    [SerializeField] private GameObject collectable;
    [SerializeField] private Collectable[] collectables;
    [SerializeField] private Collectable active;
    [SerializeField] private Collider2D c2D;
    private void Start()
    {
        c2D = GetComponent<Collider2D>();
        active = collectables[Random.Range(0, collectables.Length)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Debug.Log("Ball!");
            Ball ball = collision.GetComponent<Ball>();
            active.Collect(ball);
            collectable.transform.position = transform.GetChild(Random.Range(1, transform.childCount)).position;
            c2D.offset = collectable.transform.position - transform.position;
            active = collectables[Random.Range(0, collectables.Length)];
        }
    }
}
