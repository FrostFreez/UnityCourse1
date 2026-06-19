using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviourPunCallbacks
{
    public PhotonView pv;
    [HideInInspector] public static GameController instance;
    [SerializeField] public GameStage stage = GameStage.WaitForPlayers;
    [field: SerializeField] public InGamePlayer myPlayer { get; private set; }

    public delegate void PlayerWonDelegate(Player player, float time);
    public PlayerWonDelegate playerWon;

    public delegate void ChangeStageDelegate(GameStage newStage);
    public ChangeStageDelegate changedStage;

    [SerializeField] public float timerA;

    [SerializeField] public List<Player> playersWhoWon = new();

    public int waitTime = 5;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        pv = gameObject.GetPhotonView();
        myPlayer = new()
        {
            player = PhotonNetwork.LocalPlayer,
            controller = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity).GetComponent<PlayerController>()
        };
        if (Statistics.instance.level == 1)
        {
            ChangeStage(GameStage.WaitForPlayers);
        }
        else
        {
            ChangeStage(GameStage.Begin);
        }
    }

    private void Update()
    {
        switch (stage)
        {
            case GameStage.WaitForPlayers:
                break;
            case GameStage.Begin:
                timerA -= Time.deltaTime;
                if (timerA < 0)
                {
                    ChangeStage(GameStage.Game);
                }
                break;
            case GameStage.Game:
                Statistics.instance.time += Time.deltaTime;
                break;
            case GameStage.End:
                break;
            case GameStage.NextLevel:
                timerA -= Time.deltaTime;
                if (timerA < 0)
                {
                    Statistics.instance.level++;
                    SceneManager.LoadScene("Gameplay");
                }
                break;
        }
    }
    public void ChangeStage(GameStage newStage)
    {
        if (stage == newStage) return;
        changedStage?.Invoke(newStage);
        switch (newStage)
        {
            case GameStage.WaitForPlayers:
                break;
            case GameStage.Begin:
                timerA = waitTime;
                PhotonNetwork.CurrentRoom.IsOpen = false;
                break;
            case GameStage.Game:
                break;
            case GameStage.End:
                break;
            case GameStage.NextLevel:
                timerA = waitTime;
                break;
        }
        stage = newStage;
    }
    [PunRPC]
    public void StartGame()
    {
        ChangeStage(GameStage.Begin);
    }
    [PunRPC]
    public void PlayerWon(Player player, float time)
    {
        playersWhoWon.Add(player);
        if (player == myPlayer.player)
        {
            ChangeStage(GameStage.End);
        }
        playerWon?.Invoke(player, time);
        if (playersWhoWon.Count == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            ChangeStage(GameStage.NextLevel);
        }
    }
}

[Serializable]
public class InGamePlayer
{
    public Player player;
    public PlayerController controller;
}
public enum GameStage
{
    WaitForPlayers,
    Begin,
    Game,
    End,
    NextLevel
}