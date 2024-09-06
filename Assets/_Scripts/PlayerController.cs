using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int playerX = 2; // Начальная позиция персонажа по оси X (в комнате C5)
    private int playerY = 2; // Начальная позиция персонажа по оси Y (в комнате C5)

    public GameMap gameMap;

    // UI Кнопки
    public Button leftButton;
    public Button rightButton;
    public Button upButton;
    public Button downButton;

    void Start()
    {
        gameMap = new GameMap(); // Создаем карту
        EnterRoom(playerX, playerY); // Входим в стартовую комнату (C5)

        // Привязываем кнопки к методам
        leftButton.onClick.AddListener(() => MovePlayer(0, -1)); // Влево
        rightButton.onClick.AddListener(() => MovePlayer(0, 1)); // Вправо
        upButton.onClick.AddListener(() => MovePlayer(-1, 0));   // Вверх
        downButton.onClick.AddListener(() => MovePlayer(1, 0));  // Вниз
    }

    // Метод для входа в комнату
    void EnterRoom(int x, int y)
    {
        Debug.Log($"Player entered room at: ({x}, {y})");

        // Получаем текущую комнату
        Room currentRoom = gameMap.GetRoom(x, y);

        // Проверяем, можно ли двигаться влево и обновляем кнопку
        leftButton.interactable = currentRoom.canMoveLeft;

        // Проверяем, можно ли двигаться вправо и обновляем кнопку
        rightButton.interactable = currentRoom.canMoveRight;

        // Проверяем, можно ли двигаться вверх и обновляем кнопку
        upButton.interactable = currentRoom.canMoveUp;

        // Проверяем, можно ли двигаться вниз и обновляем кнопку
        downButton.interactable = currentRoom.canMoveDown;
    }

    // Метод для обновления позиции персонажа
    public void MovePlayer(int xChange, int yChange)
    {
        int newX = playerX + xChange; // Изменяем X для вертикальных перемещений (вверх-вниз)
        int newY = playerY + yChange; // Изменяем Y для горизонтальных перемещений (влево-вправо)

        Debug.Log($"Trying to move player to: ({newX}, {newY})");

        // Проверяем, существует ли комната на новой позиции и можно ли двигаться в эту сторону
        if (gameMap.GetRoom(newX, newY) != null)
        {
            Room currentRoom = gameMap.GetRoom(playerX, playerY);

            // Проверка для движения влево
            if (yChange == -1 && currentRoom.canMoveLeft)
            {
                playerY = newY;
            }
            // Проверка для движения вправо
            else if (yChange == 1 && currentRoom.canMoveRight)
            {
                playerY = newY;
            }
            // Проверка для движения вверх
            else if (xChange == -1 && currentRoom.canMoveUp)
            {
                playerX = newX;
            }
            // Проверка для движения вниз
            else if (xChange == 1 && currentRoom.canMoveDown)
            {
                playerX = newX;
            }

            Debug.Log($"Player moved to: ({playerX}, {playerY})");

            EnterRoom(playerX, playerY); // Входим в новую комнату и обновляем кнопки
        }
        else
        {
            Debug.Log($"No room exists at: ({newX}, {newY})");
        }
    }
}
