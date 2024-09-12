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
    [SerializeField] private GameAudioController gameAudioController;
    private int currentLevelIndex;

    private void Start()
    {
        _isTriggered = false;
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameAudioController.PlayEnemyCollision();
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
            gameAudioController.PlayKeyCOllectSound();
            _playerMovement.StopMovement();
            Destroy(collision.gameObject);
            _isTriggered = true;

            if (SceneManager.GetActiveScene().name == "GhostGame")
                currentLevelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
            else
                currentLevelIndex = PlayerPrefs.GetInt("PlaneLevelIndex", 0);

            if (_levelManager.levels.Length > currentLevelIndex + 1)
            {
                currentLevelIndex++;

                if (SceneManager.GetActiveScene().name == "GhostGame")
                {
                    PlayerPrefs.SetInt("LevelIndex", currentLevelIndex);
                    int roadIndex = PlayerPrefs.GetInt("GhostRoadSprite", 0);
                    roadIndex++;
                    if (roadIndex == 4)
                    {
                        roadIndex = 0;
                    }
                    PlayerPrefs.SetInt("GhostRoadSprite", roadIndex);

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
                    PlayerPrefs.SetInt("PlaneLevelIndex", currentLevelIndex);
                    int roadIndex = PlayerPrefs.GetInt("PlaneRoadSprite", 0);
                    roadIndex++;
                    if (roadIndex == 4)
                    {
                        roadIndex = 0;
                    }
                    PlayerPrefs.SetInt("PlaneRoadSprite", roadIndex);

                    int bestLevel = PlayerPrefs.GetInt("PlaneBestLevel", 0);
                    if (currentLevelIndex > bestLevel)
                    {
                        bestLevel = currentLevelIndex;
                        PlayerPrefs.SetInt("PlaneBestLevel", bestLevel);
                    }
                    StartCoroutine(ShowWinPanelAndLoadNextLevel());
                }
                    

                
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
        _canvasGroup.alpha = 0f;
        float duration = 1f;
        float elapsedTime = 0f;

        while (_canvasGroup.alpha < 1f)
        {
            elapsedTime += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        _canvasGroup.alpha = 1f;

        _endOfGame.SetActive(true);
    }

    private IEnumerator ShowWinPanelAndLoadNextLevel()
    {
        _canvasGroup.alpha = 0f;
        float duration = 1f;
        float elapsedTime = 0f;

        while (_canvasGroup.alpha < 1f)
        {
            elapsedTime += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        _canvasGroup.alpha = 1f;

        GameObject levelPassed = Instantiate(_levelPassedPrefab);
        gameAudioController.PLayLelveCompleteSound();
        Destroy(levelPassed, 1.9f);
        yield return new WaitForSeconds(2.5f);

        if (SceneManager.GetActiveScene().name == "GhostGame")
            SceneManager.LoadScene("GhostGame");
        else
            SceneManager.LoadScene("PlaneGame");
    }

    private IEnumerator ShowLosePanelAndReloadLevel()
    {
        gameAudioController.PlayLoseSound();

        _canvasGroup.alpha = 0f;
        float duration = 1f;
        float elapsedTime = 0f;

        while (_canvasGroup.alpha < 1f)
        {
            elapsedTime += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        _canvasGroup.alpha = 1f;
        GameObject levelFailed = Instantiate(_levelFailedPrefab);
        Destroy(levelFailed, 1.9f);
        
        yield return new WaitForSeconds(3f);

        if (SceneManager.GetActiveScene().name == "GhostGame")
            SceneManager.LoadScene("GhostGame");
        else
            SceneManager.LoadScene("PlaneGame");
    }
}