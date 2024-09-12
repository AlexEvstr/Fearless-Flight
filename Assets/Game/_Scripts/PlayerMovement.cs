using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 2f;  // Скорость движения персонажа

    // Ссылки на компоненты
    private Animator animator;
    private Transform playerTransform;
    private AudioSource footStepAudio;
    private bool isMoving = false;

    void Awake()
    {
        footStepAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
    }

    // Публичный метод для движения вверх
    public void MoveUp()
    {
        if (!isMoving)
        {
            animator.SetTrigger("MoveUp");
            StartCoroutine(MoveInDirection(Vector2.up));
        }
    }

    // Публичный метод для движения вниз
    public void MoveDown()
    {
        if (!isMoving)
        {
            animator.SetTrigger("MoveDown");
            StartCoroutine(MoveInDirection(Vector2.down));
        }
    }

    // Публичный метод для движения влево
    public void MoveLeft()
    {
        if (!isMoving)
        {
            animator.SetTrigger("MoveLeft");
            StartCoroutine(MoveInDirection(Vector2.left));
        }
    }

    // Публичный метод для движения вправо
    public void MoveRight()
    {
        if (!isMoving)
        {
            animator.SetTrigger("MoveRight");
            StartCoroutine(MoveInDirection(Vector2.right));
        }
    }

    // Корутина для движения в течение 3 секунд в выбранном направлении
    IEnumerator MoveInDirection(Vector2 direction)
    {
        footStepAudio.Play();
        isMoving = true;
        float duration = 3f;  // Продолжительность движения
        float elapsedTime = 0f;

        while (elapsedTime < duration && isMoving)
        {
            // Перемещаем персонажа вдоль осей X и Y в 2D
            playerTransform.Translate(direction * moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // После завершения движения остановим анимацию
        StopMovement();
        isMoving = false;
        footStepAudio.Stop();
    }

    public void StopMovement()
    {
        isMoving = false;
        // Если корутина движения запущена, останавливаем ее

        animator.ResetTrigger("MoveUp");
        animator.ResetTrigger("MoveDown");
        animator.ResetTrigger("MoveLeft");
        animator.ResetTrigger("MoveRight");
        animator.SetTrigger("Idle");
    }
}
