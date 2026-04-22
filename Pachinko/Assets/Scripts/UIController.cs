using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private BallSpawner spawner;
    [SerializeField] private Image inGameBall;
    [SerializeField] private Text charges;

    [SerializeField] private Transform nextBalls;
    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private Text score;

    private void Start()
    {
        inGameBall.sprite = spawner.inventory.balls[0].sprite;
        for (int i = 1; i < spawner.inventory.balls.Count; i++)
        {
            GameObject newBall = Instantiate(ballPrefab, nextBalls);
            newBall.GetComponent<Image>().sprite = spawner.inventory.balls[i].sprite;
        }

        spawner.newBallSpawned += AddToBall;
        spawner.newBallD += UpdateBall;
        Inventory.change += UpdatePoints;
    }
    public void UpdateBall()
    {
        if (spawner.nextBall)
        {
            inGameBall.sprite = spawner.nextBall.sprite;
            charges.text = spawner.nextBall[StatusType.Charges].ToString();

            if (spawner.ballSOs.Count > 0)
            {
                Destroy(nextBalls.GetChild(0).gameObject);
            }
        }
    }
    public void UpdatePoints(int points)
    {
        string text = points.ToString();
        while (text.Length < 6)
        {
            text = "0" + text;
        }
        score.text = text;
    }
    public void UpdateChargesText(StatusType type, float value)
    {
        if (type == StatusType.Charges)
        {
            charges.text = value.ToString();
        }
    }

    public void AddToBall(Ball ball)
    {
        ball.status.updateStatus += UpdateChargesText;
    }
}
