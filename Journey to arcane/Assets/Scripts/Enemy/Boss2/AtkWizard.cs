using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkWizard : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int numberOfBullets = 12;
    [SerializeField] private float angleBetweenBullets = 30f;
    [SerializeField] private float moveUpDistance = 5f; // kho?ng c?ch bay l?n
    [SerializeField] private float attackDuration = 2.5f; // th?i gian th?c hi?n ho?t ?nh t?n c?ng
    [SerializeField] private HealthWizard healthWizard;
    [SerializeField] private float soulSkillCooldown = 6f;
    [SerializeField] private GameObject soul2Prefab;

    private float cooldownTimer = Mathf.Infinity;
    private float soulSkillCooldownTimer = Mathf.Infinity;
    private bool canMoveUp = true; // bi?n theo d?i tr?ng th?i bay l?n
    private bool isAtOriginalPosition = true; // bi?n theo d?i tr?ng th?i v? tr? ban ??u

    private Animator myAnim;
    private RespawnCharacter respawnCharacter;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        soulSkillCooldownTimer += Time.deltaTime;

        if (isAtOriginalPosition)
        {
            // Di chuy?n ng?u nhi?n
            if (canMoveUp)
            {
                transform.Translate(Vector2.right * Time.deltaTime);
            }

            int randomSkillIndex = Random.Range(1, 4);

            if (healthWizard.health <= 800)
            {
                numberOfBullets = 30;
                angleBetweenBullets = 12f;
                bulletSpeed = 16f;
            }
 
            if (PlayerInSight())
            {
                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    if (healthWizard.health <= 1500)
                    {
                        if (randomSkillIndex == 1)
                        {                          
                            StartCoroutine(MoveUpAndAttack());
                        }
                        else if (randomSkillIndex == 2)
                        {
                            myAnim.SetTrigger("Atk2");
                            FireSpecialBullets();
                        }
                        else if (randomSkillIndex == 3 && soulSkillCooldownTimer >= soulSkillCooldown)
                        {
                            myAnim.SetTrigger("Atk3");
                            SpawnSouls();
                            soulSkillCooldownTimer = 0;
                        }                      
                    }
                }
            }
        }
    }
    private IEnumerator MoveUpAndAttack()
    {
        canMoveUp = false;
        isAtOriginalPosition = false;
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = originalPosition + Vector2.up * moveUpDistance;

        // Di chuy?n l?n v? tr? m?i
        float elapsed = 0f;
        while (elapsed < 1f)
        {
            transform.position = Vector2.Lerp(originalPosition, targetPosition, elapsed / 1f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        // Th?c hi?n ho?t ?nh t?n c?ng
        myAnim.SetTrigger("Atk1");
        FireBullet();
        yield return new WaitForSeconds(attackDuration);

        // Di chuy?n tr? l?i v? tr? ban ??u
        elapsed = 0f;
        while (elapsed < 1f)
        {
            transform.position = Vector2.Lerp(targetPosition, originalPosition, elapsed / 1f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;

        // Ch? 10 gi?y tr??c khi cho ph?p bay l?n l?n n?a
        yield return new WaitForSeconds(4f);
        canMoveUp = true;
        isAtOriginalPosition = true;
    }

    private void FireBullet()
    {
        float startAngle = -((numberOfBullets - 1) * angleBetweenBullets) / 2;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + i * angleBetweenBullets;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = Quaternion.Euler(0, 0, angle) * firePoint.right;
            rb.velocity = direction * 10f;

            Destroy(bullet, 8f);
        }
    }

    private void FireSpecialBullets()
    {
        // Get the player's position
        Vector2 playerPosition = respawnCharacter.transform.position;

        // Original bullet
        FireBulletFromPosition(firePoint.position, playerPosition);

        // Additional bullets from specified positions
        FireBulletFromPosition(firePoint.position + new Vector3(1, 1, 0), playerPosition);
        FireBulletFromPosition(firePoint.position + new Vector3(-1, 1, 0), playerPosition);
        FireBulletFromPosition(firePoint.position + new Vector3(1, -1, 0), playerPosition);
    }

    private void FireBulletFromPosition(Vector3 position, Vector2 targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Calculate direction towards the target position
        Vector2 direction = (targetPosition - (Vector2)position).normalized;
        rb.velocity = direction * bulletSpeed;

        Destroy(bullet, 8f);
    }
    private void SpawnSouls()
    {
        Vector3 bossPosition = transform.position;

        GameObject soul1 = Instantiate(soul2Prefab, bossPosition+ new Vector3(0, -1f, 0) + Vector3.left * 4f, Quaternion.identity);
        GameObject soul2 = Instantiate(soul2Prefab, bossPosition + new Vector3(0, -1f, 0) + Vector3.right * 4f, Quaternion.identity);

        // Destroy souls after soulSkillDuration seconds
        Destroy(soul1, 1.3f);
        Destroy(soul2, 1.3f);
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
}
