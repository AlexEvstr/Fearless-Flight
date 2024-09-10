using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameGhostController : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvas;

    private void Start()
    {
        fadeCanvas.alpha = 1;
        StartCoroutine(ShowGame());
    }

    private IEnumerator ShowGame()
    {
        yield return new WaitForSeconds(2f);
        while (fadeCanvas.alpha > 0)
        {
            fadeCanvas.alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        fadeCanvas.alpha = 0;
    }

    public void BackToHome()
    {
        SceneManager.LoadScene("GhostMenu");
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("GhostGame");
    }
}
