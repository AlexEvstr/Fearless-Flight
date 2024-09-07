using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int playerX = 2;
    private int playerY = 2;

    public GameMap gameMap;
    public CanvasGroup fadeCanvas;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _player;

    public Button leftButton;
    public Button rightButton;
    public Button upButton;
    public Button downButton;

    void Start()
    {
        gameMap = new GameMap();
        EnterRoom(playerX, playerY);

        leftButton.onClick.AddListener(() => MovePlayer(0, -1));
        rightButton.onClick.AddListener(() => MovePlayer(0, 1));
        upButton.onClick.AddListener(() => MovePlayer(-1, 0));
        downButton.onClick.AddListener(() => MovePlayer(1, 0));
    }

    void EnterRoom(int x, int y)
    {
        Debug.Log($"Player entered room at: ({x}, {y})");
        _player.transform.position = Vector2.zero;
        TurnOnButtons();

        Room currentRoom = gameMap.GetRoom(x, y);

        leftButton.interactable = currentRoom.canMoveLeft;

        rightButton.interactable = currentRoom.canMoveRight;

        upButton.interactable = currentRoom.canMoveUp;

        downButton.interactable = currentRoom.canMoveDown;
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

        Debug.Log($"Trying to move player to: ({newX}, {newY})");

        if (gameMap.GetRoom(newX, newY) != null)
        {
            Room currentRoom = gameMap.GetRoom(playerX, playerY);
            
            if (yChange == -1 && currentRoom.canMoveLeft)
            {
                playerY = newY;
                _playerMovement.MoveLeft();
            } 
            else if (yChange == 1 && currentRoom.canMoveRight)
            {
                playerY = newY;
                _playerMovement.MoveRight();
            }
            else if (xChange == -1 && currentRoom.canMoveUp)
            {
                playerX = newX;
                _playerMovement.MoveUp();
            }            
            else if (xChange == 1 && currentRoom.canMoveDown)
            {
                playerX = newX;
                _playerMovement.MoveDown();
            }

            yield return new WaitForSeconds(2.0f);

            Debug.Log($"Player moved to: ({playerX}, {playerY})");
            StartCoroutine(FadeToBlack());            
        }
        else
        {
            Debug.Log($"No room exists at: ({newX}, {newY})");
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