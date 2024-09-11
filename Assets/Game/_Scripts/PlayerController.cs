using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;  // Для простого доступа из других классов, таких как LevelManager

    public GameObject ghostPrefab;  // Префаб призрака
    public GameObject keyPrefab;    // Префаб ключа
    private GameObject currentGhost;
    private GameObject currentKey;

    private int playerX = 1;
    private int playerY = 1;

    public CanvasGroup fadeCanvas;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _player;

    public Button leftButton;
    public Button rightButton;
    public Button upButton;
    public Button downButton;

    private Room[,] rooms;  // Двумерный массив для хранения комнат уровня

    private void Awake()
    {
        Instance = this;  // Устанавливаем статический экземпляр
    }

    void Start()
    {
        // Подключение слушателей событий для кнопок движения
        leftButton.onClick.AddListener(() => MovePlayer(-1, 0));  // Влево — изменяем X
        rightButton.onClick.AddListener(() => MovePlayer(1, 0));  // Вправо — изменяем X
        upButton.onClick.AddListener(() => MovePlayer(0, 1));     // Вверх — изменяем Y
        downButton.onClick.AddListener(() => MovePlayer(0, -1));  // Вниз — изменяем Y
    }


    public void LoadLevel(LevelData levelData)
    {
        // Устанавливаем размеры уровня динамически
        rooms = new Room[levelData.width, levelData.height];

        // Загружаем комнаты из LevelData
        foreach (RoomData roomData in levelData.rooms)
        {
            rooms[roomData.roomX, roomData.roomY] = new Room(
                roomData.canMoveUp,
                roomData.canMoveDown,
                roomData.canMoveLeft,
                roomData.canMoveRight,
                roomData.hasGhost,
                roomData.hasKey,
                roomData.ghostPosition,
                roomData.keyPosition
            );
        }

        // Устанавливаем начальную позицию игрока из данных уровня
        playerX = (int)levelData.playerStartPosition.x;
        playerY = (int)levelData.playerStartPosition.y;

        // Перемещаем игрока в начальную комнату
        EnterRoom(playerX, playerY);
    }


    void EnterRoom(int x, int y)
    {
        _player.transform.position = Vector2.zero;
        TurnOnButtons();

        Room currentRoom = rooms[x, y];

        leftButton.interactable = currentRoom.canMoveLeft;
        rightButton.interactable = currentRoom.canMoveRight;
        upButton.interactable = currentRoom.canMoveUp;
        downButton.interactable = currentRoom.canMoveDown;

        HandleRoomObjects(currentRoom);
    }

    private void TurnOffButtons()
    {
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        upButton.gameObject.SetActive(false);
        downButton.gameObject.SetActive(false);
    }

    private void TurnOnButtons()
    {
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        upButton.gameObject.SetActive(true);
        downButton.gameObject.SetActive(true);
    }

    public void MovePlayer(int xChange, int yChange)
    {
        // Если xChange меняется, это движение влево/вправо
        // Если yChange меняется, это движение вверх/вниз

        StartCoroutine(MoveBehavior(xChange, yChange));
        TurnOffButtons();  // Отключаем кнопки
    }

    private IEnumerator MoveBehavior(int xChange, int yChange)
    {
        // Определяем новые координаты для игрока
        int newX = playerX + xChange;  // Влево/вправо
        int newY = playerY + yChange;  // Вверх/вниз

        Room currentRoom = rooms[playerX, playerY];

        // Проверяем возможность движения по горизонтали (xChange)
        if (xChange == -1 && currentRoom.canMoveLeft)
        {
            playerX = newX;
            _playerMovement.MoveLeft();
        }
        else if (xChange == 1 && currentRoom.canMoveRight)
        {
            playerX = newX;
            _playerMovement.MoveRight();
        }
        // Проверяем возможность движения по вертикали (yChange)
        else if (yChange == 1 && currentRoom.canMoveUp)  // Вверх (по оси y)
        {
            playerY = newY;
            _playerMovement.MoveUp();
        }
        else if (yChange == -1 && currentRoom.canMoveDown)  // Вниз (по оси y)
        {
            playerY = newY;
            _playerMovement.MoveDown();
        }

        yield return new WaitForSeconds(2.0f);

        if (!PlayerTriggerDetector._isTriggered)
        {
            StartCoroutine(FadeToBlack());
        }
    }


    private void HandleRoomObjects(Room room)
    {
        // Обработка призраков
        if (room.hasGhost)
        {
            if (currentGhost == null)
            {
                Vector2 ghostPos = room.ghostPosition.HasValue ? room.ghostPosition.Value : Vector2.zero;
                currentGhost = Instantiate(ghostPrefab, ghostPos, Quaternion.identity);
            }
        }
        else if (currentGhost != null)
        {
            Destroy(currentGhost);
        }

        // Обработка ключей
        if (room.hasKey)
        {
            if (currentKey == null)
            {
                Vector2 keyPos = room.keyPosition.HasValue ? room.keyPosition.Value : Vector2.zero;
                currentKey = Instantiate(keyPrefab, keyPos, Quaternion.identity);
            }
        }
        else if (currentKey != null)
        {
            Destroy(currentKey);
        }
    }

    private IEnumerator FadeToBlack()
    {
        while (fadeCanvas.alpha < 1)
        {
            fadeCanvas.alpha += Time.deltaTime;
            yield return null;
        }
        fadeCanvas.alpha = 1;

        EnterRoom(playerX, playerY);
        StartCoroutine(FadeToClear());
    }

    private IEnumerator FadeToClear()
    {
        while (fadeCanvas.alpha > 0)
        {
            fadeCanvas.alpha -= Time.deltaTime;
            yield return null;
        }
        fadeCanvas.alpha = 0;
    }
}
