using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;  // Массив уровней
    public TMP_Text levelText;
    private int currentLevelIndex;  // Индекс текущего уровня

    void Start()
    {
        currentLevelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
        if (currentLevelIndex < 10)
        {
            levelText.text = $"00{currentLevelIndex + 1}";
        }
        else if (currentLevelIndex >= 10 && currentLevelIndex < 100)
        {
            levelText.text = $"0{currentLevelIndex + 1}";
        }
        else
        {
            levelText.text = $"{currentLevelIndex + 1}";
        }
        
        // Загружаем первый уровень при старте игры
        LoadLevel(currentLevelIndex);
    }

    // Метод для загрузки уровня по индексу
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex > levels.Length)
        {
            Debug.LogError("Игра пройдена");
            return;
        }
        // Загружаем данные уровня и передаём их PlayerController
        PlayerController.Instance.LoadLevel(levels[levelIndex]);
        Debug.Log($"Уровень {levelIndex + 1} загружен.");
    }

    // Метод для загрузки следующего уровня
    public void LoadNextLevel()
    {
        int nextLevelIndex = currentLevelIndex + 1;
        if (nextLevelIndex < levels.Length)
        {
            LoadLevel(nextLevelIndex);
            currentLevelIndex = nextLevelIndex;
        }
        else
        {
            Debug.Log("Это был последний уровень.");
        }
    }
}
