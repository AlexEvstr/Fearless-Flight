using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;  // Массив уровней
    private int currentLevelIndex = 0;  // Индекс текущего уровня

    void Start()
    {
        // Загружаем первый уровень при старте игры
        LoadLevel(currentLevelIndex);
    }

    // Метод для загрузки уровня по индексу
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Length)
        {
            Debug.LogError("Неверный индекс уровня.");
            return;
        }

        // Загружаем данные уровня и передаём их PlayerController
        PlayerController.Instance.LoadLevel(levels[levelIndex]);
        Debug.Log($"Уровень {levelIndex} загружен.");
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
