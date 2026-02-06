using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanBehavior : MonoBehaviour
{

    [SerializeField] GameObject Player;
    Rigidbody2D rb;
    [SerializeField] int amountDamage = 10;
    [SerializeField] AudioSource hitAudioSource;
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
            hitAudioSource.Play();
            collision.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(amountDamage);
        }
    }
}
