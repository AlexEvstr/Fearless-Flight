using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameGhostController : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private Image _roadImage;
    [SerializeField] private Sprite[] _roadSprites;
    private int roadIndex;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GhostGame")
            roadIndex = PlayerPrefs.GetInt("GhostRoadSprite", 0);
        else
            roadIndex = PlayerPrefs.GetInt("PlaneRoadSprite", 0);

        _roadImage.sprite = _roadSprites[roadIndex];

        fadeCanvas.alpha = 1;
        StartCoroutine(ShowGame());
    }

    private IEnumerator ShowGame()
    {
        yield return new WaitForSeconds(2f);
        fadeCanvas.alpha = 1f;
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (fadeCanvas.alpha > 0f)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Clamp01(1f - (elapsedTime / duration));
            yield return null;
        }

        fadeCanvas.alpha = 0f;
    }

    public void BackToHome()
    {
        if (SceneManager.GetActiveScene().name == "GhostGame")
            SceneManager.LoadScene("GhostMenu");
        else
            SceneManager.LoadScene("PlaneMenu");
    }

    public void ReloadGame()
    {
        if (SceneManager.GetActiveScene().name == "GhostGame")
            SceneManager.LoadScene("GhostGame");
        else
            SceneManager.LoadScene("PlaneMenu");
    }
}