using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{
    [SerializeField] List<GameObject> layers;
    [SerializeField] List<GameObject> prefabs;
    void Start()
    {

    }

    public void CheckLevelProgression(int num_keys)
    {
        if (num_keys == 1)
        {
            DisableItems(0);
        }
        else if (num_keys == 2)
        {
            DisableItems(1);
        }
        else if (num_keys == 3)
        {
            DisableItems(2);
            GetComponent<EnemySpawner>().StartFinalLayerBehavior();
        }
    }

    void DisableItems(int index)
    {
        layers[index].gameObject.SetActive(false);
        prefabs[index].gameObject.SetActive(false);
    }

    public void EndScene(bool victory)
    {
        Destroy(gameObject);
        if (!victory)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
            SceneManager.LoadScene("VictoryScene");

    }   
}
