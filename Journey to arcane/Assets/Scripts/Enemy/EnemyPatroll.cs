using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroll : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;

    private Rigidbody2D myBody;
    private Animator myAnim;
    private Transform currentPoint;
    public float speed;

    // enemy chase
    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x && transform.localScale.x > 0)
            {
                flip();
            }
            else if (transform.position.x < playerTransform.position.x && transform.localScale.x < 0)
            {
                flip();
            }

            if (transform.position.x > playerTransform.position.x)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else if (transform.position.x < playerTransform.position.x)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
        else
        {
            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform)
            {
                myBody.velocity = new Vector2(speed, 0);
                if (transform.localScale.x < 0)
                {
                    flip();
                }
            }
            else
            {
                myBody.velocity = new Vector2(-speed, 0);
                if (transform.localScale.x > 0)
                {
                    flip();
                }
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                flip();
                currentPoint = currentPoint == pointB.transform ? pointA.transform : pointB.transform;
            }
        }
    }

    void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
