using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkBoss : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator myAnim;
    private RespawnCharacter respawnCharacter;
    [SerializeField] private Boss boss;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (boss.checkDie)
        {
            return;
        }

        cooldownTimer += Time.deltaTime;

        int randomSkillIndex = Random.Range(1, 4);

        if(randomSkillIndex == 1)
        {
            range = 0.75f;
            colliderDistance = 0.88f;
        }
        else
        {
            range = 1.7f;
            colliderDistance = 0f;
        }

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                if (boss.health <= 1000)
                {
                    myAnim.SetTrigger("Skill" + randomSkillIndex);
                    Debug.Log(range);
                    Debug.Log(colliderDistance);
                }

                if (boss != null)
                {
                    boss.StartAttack();
                }
            }
        }
        
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            Debug.Log("Player detected in sight.");
            respawnCharacter = hit.transform.GetComponent<RespawnCharacter>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight() && respawnCharacter != null)
        {
            Debug.Log("Damaging player.");
            respawnCharacter.Die();
        }
    }

    // This method should be called as an animation event at the end of the attack animation
    public void EndAttack()
    {
        if (boss != null)
        {
            boss.EndAttack();
        }
    }
}
