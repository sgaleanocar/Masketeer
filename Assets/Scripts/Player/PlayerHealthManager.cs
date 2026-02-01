using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Slider slider;
    [SerializeField] Image healthBar;
    private int num_keys;
    private float health;
    private int playerLayer;
    private int enemiesLayer;
    [SerializeField] GameObject gameManager;

    [SerializeField] private SpriteRenderer spriteRenderer;

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
        health -= amount;
        healthBar.fillAmount -= amount / 10f;
        slider.value -= amount / 10f;
        Debug.Log(healthBar.fillAmount);
        StartCoroutine(CheckInvencibility());
        StartCoroutine(Blink(1f));
        if (health <= 0f)
        {
            Debug.Log("Player dead");
            // Handle player death here
            Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, false);
            gameManager.GetComponent<ProgressionManager>().EndScene(false); 
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
        while (Time.time < end)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.08f);
        }
        spriteRenderer.enabled = true;
    }
}
