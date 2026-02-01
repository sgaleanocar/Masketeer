using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int sceneIndex;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Loading scene " + sceneIndex);
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}
