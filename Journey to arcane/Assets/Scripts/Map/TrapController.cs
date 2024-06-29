using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapController : MonoBehaviour
{
    public float RotationSpeed = 5f;
    public float SpeedofMovement = 2f;
    public Transform pointA;
    public Transform pointB;
    private Vector3 targetPoint;

    [SerializeField] RespawnCharacter respawnCharacter;
    void Start()
    {
        targetPoint = pointA.position;
    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, RotationSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            if (transform.position == pointA.position)
            {
                targetPoint = pointB.position;
            }
            else
            {
                targetPoint = pointA.position;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, RotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            respawnCharacter.Die();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.position, 0.5f);
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
