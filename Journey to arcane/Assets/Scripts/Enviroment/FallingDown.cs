using System.Collections;
using UnityEngine;

public class FallingDown : MonoBehaviour
{
    public float fallDelay = 1f;
    public float destroyDelay = 3f;
    private Vector3 startPosition;
    private Quaternion startRotation;

    [SerializeField] private Rigidbody2D myBody;
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private PlatformEffector2D platformEffector;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        myBody.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(destroyDelay);
        myCollider.enabled = false;
        if (platformEffector != null) platformEffector.enabled = false;
        myBody.bodyType = RigidbodyType2D.Kinematic;
        myBody.velocity = Vector2.zero;
        myBody.angularVelocity = 0f;
        gameObject.SetActive(false);
        FallingDownManager.instance.RespawnRock(this);
    }

    public void ResetRock()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        myCollider.enabled = true;
        if (platformEffector != null) platformEffector.enabled = true;
        gameObject.SetActive(true);
    }
}
