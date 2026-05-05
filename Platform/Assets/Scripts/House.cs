using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private bool open = false;
    [SerializeField] private int houseIndex = 0;
    [SerializeField] private bool ready = false;

    public void SetOpen(bool newState)
    {
        open = newState;
        if (anim != null)
        {
            anim.SetBool("open", newState);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (open && collision.CompareTag("Player"))
        {
            if (collision.name == "player" + houseIndex)
            {
                ready = true;
                GameController.Instance.CheckHouses();
                collision.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (open && collision.CompareTag("Player"))
        {
            if (collision.name == "player" + houseIndex)
            {
                ready = false;
                collision.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    public bool IsReady()
    {
        return ready;
    }
}
