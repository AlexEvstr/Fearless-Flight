using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerTriggerDetector : MonoBehaviour
{
    public static bool _isTriggered;
    private PlayerMovement _playerMovement;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _levelPassedPrefab;
    [SerializeField] private GameObject _levelFailedPrefab;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] private GameObject _endOfGame;
    [SerializeField] private Button[] _buttons;

    private void Start()
    {
        _isTriggered = false;
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            foreach (var item in _buttons)
            {
                item.gameObject.SetActive(false);
            }

            _playerMovement.StopMovement();
            Destroy(collision.gameObject);
            _isTriggered = true;

            StartCoroutine(ShowLosePanelAndReloadLevel());

            PlayerPrefs.SetString("ShouldShowPreGame", "no");
        }

        if (collision.gameObject.CompareTag("Key"))
        {
            _playerMovement.StopMovement();
            Destroy(collision.gameObject);
            _isTriggered = true;

            int currentLevelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
            if (_levelManager.levels.Length > currentLevelIndex + 1)
            {
                currentLevelIndex++;
                PlayerPrefs.SetInt("LevelIndex", currentLevelIndex);

                int bestLevel = PlayerPrefs.GetInt("BestLevel", 0);
                if (currentLevelIndex > bestLevel)
                {
                    bestLevel = currentLevelIndex;
                    PlayerPrefs.SetInt("BestLevel", bestLevel);
                }

                StartCoroutine(ShowWinPanelAndLoadNextLevel());
            }
            else
            {
                StartCoroutine(ShowEndOfGame());
            }

            PlayerPrefs.SetString("ShouldShowPreGame", "no");
        }
    }

    private IEnumerator ShowEndOfGame()
    {
        _canvasGroup.alpha = 0;
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        _canvasGroup.alpha = 1;

        _endOfGame.SetActive(true);
    }

    private IEnumerator ShowWinPanelAndLoadNextLevel()
    {
        _canvasGroup.alpha = 0;
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        _canvasGroup.alpha = 1;
        GameObject levelPassed = Instantiate(_levelPassedPrefab);
        Destroy(levelPassed, 1.9f);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GhostGame");
    }

    private IEnumerator ShowLosePanelAndReloadLevel()
    {
        _canvasGroup.alpha = 0;
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        _canvasGroup.alpha = 1;
        GameObject levelFailed = Instantiate(_levelFailedPrefab);
        Destroy(levelFailed, 1.9f);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GhostGame");
    }
}
