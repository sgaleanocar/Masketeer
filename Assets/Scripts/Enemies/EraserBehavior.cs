using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserBehavior : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float defaultSpeed = 5f;
    [SerializeField] private float groundDistance = 5f;
    [SerializeField] private float attackMultiplier = 1.5f;
    [SerializeField] private int amountDamage = 10;
    private bool busy = false;

    Rigidbody2D rb;
    private GameObject elevationPoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        elevationPoint = GameObject.Find("AscendPoint");
        StartCoroutine(StartAttackCycle());
    }
    IEnumerator StartAttackCycle()
    {

        while (true) {
            if (!busy)
            {
                busy = true;
                yield return new WaitForSeconds(2f);
                StartCoroutine(AttackCycle());
            }
    
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
        while (transform.position.y < endPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, defaultSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        // Attack
        yield return new WaitForSeconds(2f);
        busy = false;
    }


}
