using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class bullet : MonoBehaviour
{
    public float speed;
    public int damage = 40;
    public Animator myAnim;
    private Rigidbody2D myBody;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myBody.velocity = transform.right * speed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        myAnim.SetTrigger("Hit");
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
