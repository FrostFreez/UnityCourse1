using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        GameController.Instance.coins.Add(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        GameController.Instance.CheckCoins();
    }
}
