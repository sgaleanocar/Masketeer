using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

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


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            num_keys++;
            Debug.Log("Keys collected: " + num_keys);
            other.gameObject.GetComponent<KeyManager>().HandlePlayerCollision(num_keys);
        }
        if (other.gameObject.CompareTag("Tool"))
        {
            gameObject.GetComponent<PlayerToolManager>().ActivateTool();
            Debug.Log("Tool collected");
            Destroy(other.gameObject);
        }
        
    }
}
