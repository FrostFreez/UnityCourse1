using UnityEngine;
using UnityEngine.UI;

public class ScreenUIManager : MonoBehaviour
{
    [SerializeField] private GameController controller;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Text timer;
    [SerializeField] private Text ability1Counter;
    [SerializeField] private Text ability2Counter;

    private void Awake()
    {
        controller = FindAnyObjectByType<GameController>();
        controller.winPanel = winPanel;
        controller.pausePanel = pausePanel;
        controller.timer = timer;
        controller.players[0].GetComponent<AbilityUIManager>().countText = ability1Counter;
        controller.players[1].GetComponent<AbilityUIManager>().countText = ability2Counter;
        winPanel.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(controller.LoadHomeScreen);
        winPanel.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(controller.ReloadLevel);
        if (controller.levelIndex == 14)
        {
            winPanel.transform.GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(controller.LoadGoodEnding);
        }
        else
        {
            winPanel.transform.GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(controller.LoadNextLevel);
        }

        pausePanel.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(controller.LoadHomeScreen);
        pausePanel.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(controller.ReloadLevel);
        pausePanel.transform.GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(controller.UnpauseGame);
    }
}
