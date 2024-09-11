using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;
    public TMP_Text levelText;
    private int currentLevelIndex;

    void Start()
    {
        currentLevelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
        if (currentLevelIndex +1 < 10)
        {
            levelText.text = $"00{currentLevelIndex + 1}";
        }
        else if (currentLevelIndex + 1 >= 10 && currentLevelIndex + 1 < 100)
        {
            levelText.text = $"0{currentLevelIndex + 1}";
        }
        else
        {
            levelText.text = $"{currentLevelIndex + 1}";
        }
        
        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex > levels.Length)
        {
            return;
        }
        PlayerController.Instance.LoadLevel(levels[levelIndex]);
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = currentLevelIndex + 1;
        if (nextLevelIndex < levels.Length)
        {
            LoadLevel(nextLevelIndex);
            currentLevelIndex = nextLevelIndex;
        }
    }
}