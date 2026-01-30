using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int num_keys;

    void Start()
    {
        num_keys = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Edge"))
        {
            collision.GetComponent<EdgeManager>().HandlePlayerCollision(
                num_keys: num_keys
            );
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            num_keys++;
            Destroy(other.gameObject);
        }
        
    }
}
