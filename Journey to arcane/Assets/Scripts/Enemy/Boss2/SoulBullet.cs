using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit by bullet.");
            Destroy(gameObject);
        }
        else if (!collision.gameObject.CompareTag("Enemy") || !collision.gameObject.CompareTag("Obstacle"))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
