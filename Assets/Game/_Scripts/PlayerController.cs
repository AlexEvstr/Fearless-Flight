using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public GameObject ghostPrefab;
    public GameObject keyPrefab;
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

    private Room[,] rooms;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        leftButton.onClick.AddListener(() => MovePlayer(-1, 0));
        rightButton.onClick.AddListener(() => MovePlayer(1, 0));
        upButton.onClick.AddListener(() => MovePlayer(0, 1));
        downButton.onClick.AddListener(() => MovePlayer(0, -1));
    }


    public void LoadLevel(LevelData levelData)
    {
        rooms = new Room[levelData.width, levelData.height];

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

        playerX = (int)levelData.playerStartPosition.x;
        playerY = (int)levelData.playerStartPosition.y;

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

        StartCoroutine(MoveBehavior(xChange, yChange));
        TurnOffButtons();
    }

    private IEnumerator MoveBehavior(int xChange, int yChange)
    {
        int newX = playerX + xChange;
        int newY = playerY + yChange;

        Room currentRoom = rooms[playerX, playerY];

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
        else if (yChange == 1 && currentRoom.canMoveUp)
        {
            playerY = newY;
            _playerMovement.MoveUp();
        }
        else if (yChange == -1 && currentRoom.canMoveDown)
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