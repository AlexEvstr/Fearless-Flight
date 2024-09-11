using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriggerDetector : MonoBehaviour
{
    public static bool _isTriggered;
    private PlayerMovement _playerMovement;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _levelPassedPrefab;
    [SerializeField] private GameObject _levelFailedPrefab;

    private void Start()
    {
        _isTriggered = false;
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Lose");

            _playerMovement.StopMovement();
            Destroy(collision.gameObject);
            _isTriggered = true;

            StartCoroutine(ShowLosePanelAndLoadNextLevel());
        }

        if (collision.gameObject.CompareTag("Key"))
        {
            Debug.Log("Win!");

            int currentLevelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
            currentLevelIndex++;
            PlayerPrefs.SetInt("LevelIndex", currentLevelIndex);

            _playerMovement.StopMovement();
            Destroy(collision.gameObject);
            _isTriggered = true;
            StartCoroutine(ShowWinPanelAndLoadNextLevel());
        }
    }

    private IEnumerator ShowWinPanelAndLoadNextLevel()
    {
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        _canvasGroup.alpha = 1;
        GameObject levelPassed = Instantiate(_levelPassedPrefab);
        Destroy(levelPassed, 1.9f);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GhostGame");
    }

    private IEnumerator ShowLosePanelAndLoadNextLevel()
    {
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        _canvasGroup.alpha = 1;
        GameObject levelFailed = Instantiate(_levelFailedPrefab);
        Destroy(levelFailed, 1.9f);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GhostGame");
    }
}
