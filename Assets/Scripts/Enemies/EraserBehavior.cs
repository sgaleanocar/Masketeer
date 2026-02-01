using System.Collections;
using UnityEngine;

public class EraserBehavior : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float defaultSpeed = 5f;
    [SerializeField] private float groundDistance = 5f;
    [SerializeField] private int amountDamage = 3;
    [SerializeField] private float playerAttackSpeed = 7f;
    [SerializeField] private float waitForAttack = 1f;

    [SerializeField] private float erraserDistance = 0.5f;
    private bool busy = false;
    private Transform playerTransform;

    Rigidbody2D rb;
    private GameObject elevationPoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        elevationPoint = GameObject.Find("AscendPoint");
    }

    public void StartEraserBehavior(Transform playerTransform, GameObject gameManager)
    {
        this.playerTransform = playerTransform;
        StartCoroutine(StartAttackCycle(gameManager));
    }
    IEnumerator StartAttackCycle(GameObject gameManager)
    {

        while (gameManager != null)
        {
            if (!busy)
            {
                busy = true;
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(AttackCycle());
            }
            yield return null;
        }
    }

    IEnumerator AttackCycle()
    {

        // Ascend
        var endPosition = new Vector3(
            transform.position.x,
            elevationPoint.transform.position.y,
            transform.position.z
        );
        rb.simulated = false;
        while (transform.position.y < endPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, defaultSpeed * Time.deltaTime);
            yield return null;
        }
        var playerPos = playerTransform.position;
        yield return new WaitForSeconds(waitForAttack);
        // Attack
        while (Vector3.Distance(transform.position, playerPos) > erraserDistance)
        {

            transform.position = Vector3.MoveTowards(
                transform.position, playerPos,
                playerAttackSpeed * Time.deltaTime
            );
            yield return null;
        }
        rb.simulated = true;
        yield return new WaitForSeconds(3f);
        busy = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle collision with player
            var playerHealth = collision.gameObject.GetComponent<PlayerHealthManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(amountDamage);
            }
        }
    }


}
