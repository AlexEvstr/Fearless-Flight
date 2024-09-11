using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float _speed = 2.5f;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(MoveToPlayer(player));
    }

    private IEnumerator MoveToPlayer(GameObject player)
    {
        while(true)
        {
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = player.transform.position;
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, _speed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
