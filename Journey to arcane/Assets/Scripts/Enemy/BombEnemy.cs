using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    [SerializeField] GameObject exp;
    [SerializeField] PlayerController playerController;
    private Animator myAnim;
    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            myAnim.SetTrigger("Kaboom");
            StartCoroutine(Explorer(2f));
        }
    }

    private IEnumerator Explorer(float duration)
    {
        yield return new WaitForSeconds(duration);
        
            Destroy(gameObject);
        
    }

    public void StartDam()
    {
        exp.SetActive(true);
    }
}
