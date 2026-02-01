using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsBehavior : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float defaultSpeed = 5f;
    [SerializeField] private float groundDistance = 5f;
    [SerializeField] private float attackMultiplier = 1.5f;
    [SerializeField] private int amountDamage = 2;
    [SerializeField] private Transform frontGroundDetector;
    private Animator animator;

    public LayerMask groundLayer;



    private bool touchesGround = false;
    private float currentMultiplier = 1f;
    private int dir = 1;
    private float lastCheck = 0f;
    

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lastCheck = -999f;
        animator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        if (touchesGround)
        {
            rb.linearVelocity = new Vector2(
                currentMultiplier * defaultSpeed * dir,
                rb.linearVelocity.y
            );
            ValidateFrontGround();
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
            
    }

    void ValidateFrontGround()
    {
        if (Time.time - lastCheck < 2f)
            return;

        Debug.Log("Checking ground ahead");
        lastCheck = Time.time;
        bool hasGroundAhead = Physics2D.Raycast(
            frontGroundDetector.position,
            Vector2.down,
            groundDistance,
            groundLayer
        );

        if (!hasGroundAhead)
            TurnAround();
        else
            Debug.Log("Ground ahead detected");
    }

    void TurnAround()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        dir *= -1;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentMultiplier = attackMultiplier;
            if (collision.transform.position.x > transform.position.x)
            {
                TurnAround();
            }
            animator.SetBool("isAttacking", true);
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentMultiplier = 1f;
            animator.SetBool("isAttacking", false);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Quitar vida");
            other.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(amountDamage);
            animator.SetBool("damageMade", true);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            touchesGround = true;
        }
    }
    
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            {
                touchesGround = false;
            }
    }
}
