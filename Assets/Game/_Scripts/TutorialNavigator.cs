using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialNavigator : MonoBehaviour
{
    private float moveSpeed = 2f;

    private Animator animator;
    private Transform playerTransform;
    [SerializeField] private GameObject[] _tutorialScreens;
    [SerializeField] private GameObject[] _buttonsScreen1;
    [SerializeField] private GameObject[] _buttonsScreen2;
    [SerializeField] private GameObject[] _buttonsScreen3;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _ghost;
    [SerializeField] private GameObject[] _congrats;
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject _mainMenu;

    private bool isMoving = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();

        foreach (var item in _buttonsScreen1)
        {
            item.SetActive(true);
        }
        foreach (var item in _buttonsScreen2)
        {
            item.SetActive(true);
        }
        foreach (var item in _buttonsScreen3)
        {
            item.SetActive(true);
        }
    }

    public void MoveUp()
    {
        if (!isMoving)
        {
            animator.SetTrigger("MoveUp");
            StartCoroutine(MoveInDirection(Vector2.up));
            foreach (var item in _buttonsScreen1)
            {
                item.SetActive(false);
            }
                StartCoroutine(GoTo2Screen());
        }
    }

    private IEnumerator GoTo2Screen()
    {
        yield return new WaitForSeconds(2.0f);
        _canvasGroup.alpha = 0;
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        _canvasGroup.alpha = 1;
        
        _tutorialScreens[1].SetActive(true);

        yield return new WaitForSeconds(0.5f);

        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        _canvasGroup.alpha = 0;
        _tutorialScreens[0].SetActive(false);
    }

    private IEnumerator GoTo3Screen()
    {
        yield return new WaitForSeconds(2.0f);
        _canvasGroup.alpha = 0;
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        _canvasGroup.alpha = 1;
        
        _tutorialScreens[2].SetActive(true);

        yield return new WaitForSeconds(0.5f);

        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        _canvasGroup.alpha = 0;
        _tutorialScreens[1].SetActive(false);
    }

    public void MoveDown()
    {
        if (!isMoving)
        {
            animator.SetTrigger("MoveDown");
            StartCoroutine(MoveInDirection(Vector2.down));

            foreach (var item in _buttonsScreen3)
            {
                item.SetActive(false);
            }
        }
    }

    public void MoveLeft()
    {
        if (!isMoving)
        {
            animator.SetTrigger("MoveLeft");
            StartCoroutine(MoveInDirection(Vector2.left));
        }
    }

    public void MoveRight()
    {
        if (!isMoving)
        {
            animator.SetTrigger("MoveRight");
            StartCoroutine(MoveInDirection(Vector2.right));
            _ghost.GetComponent<EnemyMovement>().enabled = true;
            foreach (var item in _buttonsScreen2)
            {
                item.SetActive(false);
            }

            StartCoroutine(GoTo3Screen());
        }
    }

    IEnumerator MoveInDirection(Vector2 direction)
    {
        isMoving = true;
        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration && isMoving)
        {
            playerTransform.Translate(direction * moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StopMovement();
        isMoving = false;
    }

    public void StopMovement()
    {
        isMoving = false;

        animator.ResetTrigger("MoveUp");
        animator.ResetTrigger("MoveDown");
        animator.ResetTrigger("MoveLeft");
        animator.ResetTrigger("MoveRight");
        animator.SetTrigger("Idle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            StopMovement();
            Destroy(collision.gameObject);
            foreach (var item in _congrats)
            {
                item.SetActive(true);
            }
        }
    }

    public void CloseTutotial()
    {
        if (SceneManager.GetActiveScene().name == "GhostMenu")
            SceneManager.LoadScene("GhostMenu");
        else
            SceneManager.LoadScene("PlaneMenu");
    }
}