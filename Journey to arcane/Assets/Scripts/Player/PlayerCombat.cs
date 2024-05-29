using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator myAnim;

    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            Attack();
        }
    }

    void Attack()
    {
        myAnim.SetTrigger("Attack");
    }
}
