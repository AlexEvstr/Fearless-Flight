using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 2f;

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

    public void MoveUp()
    {
        if (!isMoving)
        {
            animator.SetTrigger("MoveUp");
            StartCoroutine(MoveInDirection(Vector2.up));
        }
    }

    public void MoveDown()
    {
        if (!isMoving)
        {
            animator.SetTrigger("MoveDown");
            StartCoroutine(MoveInDirection(Vector2.down));
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
        }
    }

    IEnumerator MoveInDirection(Vector2 direction)
    {
        footStepAudio.Play();
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
        footStepAudio.Stop();
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
}