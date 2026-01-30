using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBehavior : MonoBehaviour
{

    [SerializeField] Animator animator;
    Rigidbody2D rb;
    [SerializeField] int amountDamage = 1;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Quitar vida");
            collision.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(amountDamage);
        }
        rb.simulated = false;
        animator.SetBool("Splashed", true);
    }
}
