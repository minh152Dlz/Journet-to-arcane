using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    RespawnCharacter respawnCharacter;
    Animator myAnim;
    Collider2D coll;
    SpriteRenderer spr;
    private void Awake()
    {
        respawnCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<RespawnCharacter>();
        myAnim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        spr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spr.enabled = true;
            if (coll.enabled)
            {
                myAnim.SetTrigger("Up");
            }           
            respawnCharacter.UpdateCheckpoint(transform.position);
            coll.enabled = false;
        }
    }
}
