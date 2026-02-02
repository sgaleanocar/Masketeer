using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int sceneIndex;
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(RebindInput());
    }

    System.Collections.IEnumerator RebindInput()
    {
        var playerInput = GetComponent<PlayerInput>();
        playerInput.enabled = false;
        yield return new WaitForEndOfFrame(); // Espera un frame
        playerInput.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.anyKey.isPressed)
        {
            GetComponent<PlayerInput>().enabled = false; 
            Debug.Log("Loading scene " + sceneIndex);
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}
