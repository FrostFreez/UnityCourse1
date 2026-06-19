using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text timer;
    [SerializeField] private Transform scores;

    [SerializeField] private GameObject scorePrefab;
    private void Awake()
    {
        GameController.instance.playerWon += PlayerWon;
        GameController.instance.changedStage += ChangeStage;
    }

    private void Update()
    {
        switch (GameController.instance.stage)
        {
            case GameStage.WaitForPlayers:
                timer.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
                break;
            case GameStage.Begin:
                timer.text = Statistics.Seconds(GameController.instance.timerA, 6);
                timer.color = Color.Lerp(Color.green, Color.red, GameController.instance.timerA / GameController.instance.waitTime);
                break;
            case GameStage.Game:
                timer.text = Statistics.Seconds(Statistics.instance.time, 6);
                break;
            case GameStage.NextLevel:
                timer.text = Statistics.Seconds(GameController.instance.timerA, 6);
                break;
        }
    }

    public void ChangeStage(GameStage newStage)
    {
        switch (newStage)
        {
            case GameStage.WaitForPlayers:
                timer.color = Color.black;
                break;
            case GameStage.Begin:
                break;
            case GameStage.Game:
                timer.color = Color.hotPink;
                break;
            case GameStage.End:
                timer.color = Color.lightCyan;
                break;
            case GameStage.NextLevel:
                break;
        }
    }

    private void PlayerWon(Player player, float time)
    {
        Transform newScore = Instantiate(scorePrefab, scores).transform;
        newScore.GetChild(0).GetComponent<Text>().text = player.NickName;
        newScore.GetChild(1).GetComponent<Text>().text = Statistics.Seconds(time, 6);
        if (player == GameController.instance.myPlayer.player)
        {
            scores.gameObject.SetActive(true);
        }
    }
}