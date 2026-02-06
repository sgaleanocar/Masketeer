using UnityEngine;

public class ToolBehavior : MonoBehaviour
{

    private Animator animator; 
    void Awake()
    {
        
    }

    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("Landed", true);
        }
    }
}