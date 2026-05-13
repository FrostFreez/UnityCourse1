using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private Button[] statsButtons;
    [SerializeField] private Text[] statsTimes;
    private void Start()
    {
        if (buttons != null)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (Statistics.Instance.furthersLevel >= i)
                {
                    buttons[i].interactable = true;
                    buttons[i].transform.GetChild(1).gameObject.SetActive(false);
                    statsButtons[i].interactable = true;
                    statsButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    buttons[i].interactable = false;
                    buttons[i].transform.GetChild(1).gameObject.SetActive(true);
                    statsButtons[i].interactable = false;
                    statsButtons[i].transform.GetChild(1).gameObject.SetActive(true);
                }
                if (Statistics.Instance.fastestTime[i] != float.MaxValue)
                {
                    string text = Statistics.Instance.fastestTime[i].ToString();
                    int j = 0;
                    while (j < text.Length)
                    {
                        if (text[j] == '.')
                        {
                            j += 2;
                            break;
                        }
                        j++;
                    }
                    if (j < 5) j = 5;
                    text = text.PadRight(5)[..j] + "s";
                    statsTimes[i].text = text;
                }
                else
                {
                    statsTimes[i].text = "N/A";
                }
            }
        }
    }
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
    }
    public void LoadHomeScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
