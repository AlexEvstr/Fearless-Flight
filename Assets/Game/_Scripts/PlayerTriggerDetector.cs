using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Key"))
        {
            Debug.Log("Key!");
            Destroy(collision.gameObject);
        }
    }
}
