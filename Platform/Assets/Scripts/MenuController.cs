using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
    }
    public void LoadHomeScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
