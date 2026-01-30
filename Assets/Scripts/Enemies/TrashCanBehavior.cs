using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanBehavior : MonoBehaviour
{

    [SerializeField] GameObject Player;
    Rigidbody2D rb;
    [SerializeField] int amountDamage = 10;
    void Awake()
    {
        transform.position = new Vector2(Player.transform.position.x, transform.position.y);
    }

    void Update()
    {
        transform.position = new Vector2(Player.transform.position.x, transform.position.y);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(amountDamage);
        }
    }
}
