using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseGameMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void OpenGhostMenu()
    {
        SceneManager.LoadScene("GhostMenu");
    }

    public void OpenPlaneMenu()
    {
        SceneManager.LoadScene("PlaneMenu");
    }
}