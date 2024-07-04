using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBurn : MonoBehaviour
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
        Debug.Log("sdasda");
        Debug.Log(hitCount);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("bulelt");
            hitCount++;
            if (hitCount >= 2)
            {
                StartCoroutine(BurnAndDestroy());
            }
        }
    }

    private IEnumerator BurnAndDestroy()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        myAnim.SetTrigger("Burn");

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
