using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Image healthBar;
    private int num_keys;
    private float health;
    private int playerLayer;
    private int enemiesLayer;
    [SerializeField] GameObject gameManager;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Animator animator;
    [SerializeField] private bool playerDeath = false;

    void Start()
    {
        health = 10f;
        playerLayer = LayerMask.NameToLayer("Player");
        enemiesLayer = LayerMask.NameToLayer("Enemies");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
{
    if (playerDeath) return;

    health -= amount;
    healthBar.fillAmount = Mathf.Clamp01(health / 10f);

    StartCoroutine(CheckInvencibility());
    StartCoroutine(Blink(1f));

    if (health <= 0f)
    {
        playerDeath = true;
        Debug.Log("Player dead");

        // Asegurar que el sprite quede visible (por si Blink lo apagó)
        spriteRenderer.enabled = true;

        animator.SetTrigger("Dies");

        // Restaurar colisiones (por si estaban ignoradas)
        Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, false);

        var progresionManager = gameManager.GetComponent<ProgressionManager>();
        StartCoroutine(progresionManager.EndScene(false));
    }
}

    IEnumerator CheckInvencibility()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, true);

        yield return new WaitForSeconds(1f);

        Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, false);
    }

    IEnumerator Blink(float t)
    {
        float end = Time.time + t;

        while (Time.time < end && !playerDeath)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.08f);
        }

        spriteRenderer.enabled = true;
    }
}
