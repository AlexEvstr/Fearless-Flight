using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameGhostController : MonoBehaviour
{
    [SerializeField] private Image _blackScreen;

    private void Start()
    {
        _blackScreen.color = new Color(0, 0, 0, 1.0f);
        StartCoroutine(ShowGame());
    }

    private IEnumerator ShowGame()
    {
        yield return new WaitForSeconds(2f);
        float aIndex = 1.0f;
        while (aIndex > 0)
        {
            _blackScreen.color = new Color(0, 0, 0, aIndex);
            aIndex -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
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
