using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Image audioOn;
    [SerializeField] private Image audioOff;
    [SerializeField] private Image _background;
    [SerializeField] private Sprite[] _backgroundSprites;
    [SerializeField] private GameObject _menuGroup;
    [SerializeField] private GameObject _settingsGroup;
    [SerializeField] private GameObject _tutorialGroup;

    private int _backgroundIndex;

    private void Start()
    {
        _backgroundIndex = PlayerPrefs.GetInt("BackgroundIndex", 0);
        _background.sprite = _backgroundSprites[_backgroundIndex];

        AudioListener.volume = PlayerPrefs.GetFloat("AudioVolume", 1);
        if (AudioListener.volume == 1)
        {
            audioOff.color = new Color(1, 1, 1, 0.3f);
        }
        else
        {
            audioOn.color = new Color(1, 1, 1, 0.3f);
        }
    }

    public void OpenGhostGame()
    {
        SceneManager.LoadScene("GhostGame");
    }

    public void OpenPlaneGame()
    {
        SceneManager.LoadScene("PlaneGame");
    }

    public void BackToEnterScene()
    {
        SceneManager.LoadScene("Enter");
    }

    public void AudioOnBtn()
    {
        AudioListener.volume = 1;
        audioOn.color = new Color(1, 1, 1, 1);
        audioOff.color = new Color(1, 1, 1, 0.3f);
        PlayerPrefs.SetFloat("AudioVolume", AudioListener.volume);
    }

    public void AudioOffBtn()
    {
        AudioListener.volume = 0;
        audioOff.color = new Color(1, 1, 1, 1);
        audioOn.color = new Color(1, 1, 1, 0.3f);
        PlayerPrefs.SetFloat("AudioVolume", AudioListener.volume);
    }

    public void ChangeBackgroundBtn()
    {
        _backgroundIndex++;
        if (_backgroundIndex == _backgroundSprites.Length)
        {
            _backgroundIndex = 0;
        }
        _background.sprite = _backgroundSprites[_backgroundIndex];
        PlayerPrefs.SetInt("BackgroundIndex", _backgroundIndex);
    }

    public void OpenSettings()
    {
        _menuGroup.SetActive(false);
        _settingsGroup.SetActive(true);
    }

    public void CloseSettings()
    {
        _settingsGroup.SetActive(false);
        _menuGroup.SetActive(true);
    }

    public void OpenTutorial()
    {
        _menuGroup.SetActive(false);
        _tutorialGroup.SetActive(true);
    }

    public void CloseTutorial()
    {
        _tutorialGroup.SetActive(false);
        _menuGroup.SetActive(true);
    }
}
