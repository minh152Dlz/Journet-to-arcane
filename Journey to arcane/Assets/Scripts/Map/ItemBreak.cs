using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBreak : MonoBehaviour
{
    private int hitCount;
    private Animator myAnim;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
        hitCount = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitCount++;
            if (hitCount >= 2)
            {
                StartCoroutine(BreakAndDestroy());
            }
        }
    }

    private IEnumerator BreakAndDestroy()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        myAnim.SetTrigger("Break");

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
