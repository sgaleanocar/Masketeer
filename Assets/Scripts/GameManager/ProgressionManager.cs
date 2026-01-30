using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    [SerializeField] List<GameObject> layers;
    [SerializeField] List<GameObject> prefabs;
    void Start()
    {

    }

    public void CheckLevelProgression(int num_keys, TriggerType type)
    {
        if (type == TriggerType.right && num_keys == 1)
        {
            DisableItems(0);
        }
        else if (type == TriggerType.left && num_keys == 2)
        {
            DisableItems(1);
        }
        else if (type == TriggerType.right && num_keys == 3)
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
}
