using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PreGameGhostController : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Sprite[] _backgroundSprites;
    [SerializeField] private GameObject _preGame;
    [SerializeField] private Image _whiteScreen;
    [SerializeField] private Image _blackScreen;
    [SerializeField] private GameObject _mainGame;

    private void OnEnable()
    {
        _preGame.SetActive(true);
        _background.sprite = _backgroundSprites[PlayerPrefs.GetInt("BackgroundIndex", 0)];
    }

    private void Start()
    {
        string shouldShowPreGame = PlayerPrefs.GetString("ShouldShowPreGame", "yes");
        if (shouldShowPreGame == "yes")
            StartCoroutine(ShowWhiteScreen());
        else
        {
            _mainGame.SetActive(true);
            _preGame.SetActive(false);
        }
    }

    private IEnumerator ShowWhiteScreen()
    {
        float aIndex = 0f;
        yield return new WaitForSeconds(6.5f);

        while (aIndex < 1)
        {
            _whiteScreen.color = new Color(1, 1, 1, aIndex);
            aIndex += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        _blackScreen.color = new Color(0, 0, 0, 1);
        _mainGame.SetActive(true);
        _preGame.SetActive(false);
    }
}