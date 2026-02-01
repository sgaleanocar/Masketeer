using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 8f;
    public float jumpForce = 14f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform spriteTransform;
    float prevLocalX;
    PlayerToolManager toolManager;
    private float currentYpos;
    private float lastYpos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        prevLocalX = spriteTransform.localScale.x;
        toolManager = GetComponent<PlayerToolManager>();
        currentYpos = transform.position.y;
        lastYpos = transform.position.y;
        isGrounded = false;
    }

    // --- New Input System ---
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().x;
        if (moveInput != 0)
        {
            spriteTransform.localScale = new Vector2(
                moveInput,
                spriteTransform.localScale.y
            );
            animator.SetBool("isMoving", true);

        }
        else
        { 
            animator.SetBool("isMoving", false);  
        }
        
    }

    void Update()
    {
        // Update animator jumping state
        lastYpos = currentYpos;
        currentYpos = transform.position.y;
        if (!isGrounded)
        {
            if (currentYpos <= lastYpos)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
            }
        }
        else
        {
            if (animator.GetBool("isFalling"))
            {
                animator.SetBool("isLanding", true);
            }
            animator.SetBool("isFalling", false);
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
        if (toolManager.hasTool)
        {
            toolManager.UseTool();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    // --- Ground check with TAG ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            isGrounded = false;
    }
}
