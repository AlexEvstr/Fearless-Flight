using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            if (SceneManager.GetActiveScene().name == "PlaneGame")
            {
                Vector2 direction = targetPosition - currentPosition;

                // Вычисляем угол для поворота и добавляем корректировку в 90 градусов
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

                // Поворачиваем врага в сторону игрока с корректировкой
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}