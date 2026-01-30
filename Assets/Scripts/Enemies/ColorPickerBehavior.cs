using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerBehavior : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField] Transform spriteTransform;
    [SerializeField] GameObject dropPrefab; 
    [SerializeField] int maxDrops = 8;
    [SerializeField] Transform dropPoint;
    [SerializeField] Animator animator;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite HalfSprite;
    [SerializeField] Sprite QuarterSprite;
    [SerializeField] Sprite EmptySprite;

    private bool fliped = false;
    private bool isEmpty = false;
    private int availableDrops;
    private bool destroySelf = false;

    void Awake()
    {
        availableDrops = maxDrops;
        StartCoroutine(DropsSpawn());
    }

    void DropHandler()
    {
        Instantiate(dropPrefab, dropPoint.position, Quaternion.identity);
        availableDrops--;
        if (availableDrops == maxDrops / 2)
        {
            spriteRenderer.sprite = HalfSprite;
        }
        else if (availableDrops == maxDrops/4)
        {
            spriteRenderer.sprite = QuarterSprite;
        }
        else if (availableDrops == 0)
        {
            spriteRenderer.sprite = EmptySprite;
            isEmpty = true;
        }
            
    }


    IEnumerator DropsSpawn()
    {
        while (true)
        {

            var num = Random.Range(1, 3);
            yield return new WaitForSeconds(num);
            DropHandler();
            if (isEmpty)
            {
                yield return new WaitForSeconds(2);
                destroySelf = true;
                break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Edge") && !fliped)
        {
            transform.localScale *= new Vector2(-1f, 1f);
            spriteTransform.localScale *= new Vector2(-1f, 1f);
            fliped = true;
        }
    }

    void Update()
    {
        if (destroySelf)
        {
            Destroy(gameObject);
        }
    }

    
    


    
}
