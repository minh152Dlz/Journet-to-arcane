using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator myAnim;
    RespawnCharacter respawnCharacter;
    [SerializeField] EnemyPatroll enemyPatroll;
    private void Awake()
    {
        //sceneController = sceneController.instance;
        myAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            enemyPatroll.speed = 0;
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                myAnim.SetTrigger("meleeAtk");
                
                if (enemyPatroll != null)
                {
                    enemyPatroll.StartAttack();
                }
            }
        }
        else
        {
            enemyPatroll.speed = 2;
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3( boxCollider.bounds.size.x* range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        

        if(hit.collider != null)
        {
            Debug.Log("Player detected in sight.");
            respawnCharacter = hit.transform.GetComponent<RespawnCharacter>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight() && respawnCharacter != null)
        {
            Debug.Log("Damaging player.");
            respawnCharacter.Die();
        }
    }

    private void FinishAttack()
    {
        if (enemyPatroll != null)
        {
            enemyPatroll.FinishAttack(); 
        }
    }
}
    