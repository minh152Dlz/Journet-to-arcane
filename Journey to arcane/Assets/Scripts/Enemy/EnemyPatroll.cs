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
    private bool isAttacking;
    [SerializeField] GameObject warn;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            myBody.velocity = Vector2.zero;
            return;
        }
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < chaseDistance && IsPlayerWithinPatrolArea())
        {
            isChasing = true;
            warn.SetActive(true);
        }
        else
        {
            isChasing = false;
            warn.SetActive(false);
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
            }else if (transform.position.x == playerTransform.position.x)
            {
                return;
            }

            if (transform.position.x > playerTransform.position.x)
            {
                transform.position += Vector3.left * 6f * Time.deltaTime;
            }
            else if (transform.position.x < playerTransform.position.x)
            {
                transform.position += Vector3.right * 6f * Time.deltaTime;
            }

            if (transform.position.x < pointA.transform.position.x)
            {
                transform.position = new Vector3(pointA.transform.position.x, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > pointB.transform.position.x)
            {
                transform.position = new Vector3(pointB.transform.position.x, transform.position.y, transform.position.z);
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
    bool IsPlayerWithinPatrolArea()
    {
        return playerTransform.position.x >= pointA.transform.position.x && playerTransform.position.x <= pointB.transform.position.x;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    public void StartAttack()
    {
        isAttacking = true;
        myAnim.SetTrigger("meleeAtk"); 
    }
    public void FinishAttack()
    {
        isAttacking = false;
    }
}
