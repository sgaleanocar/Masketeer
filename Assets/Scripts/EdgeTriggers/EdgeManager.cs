using UnityEngine;

public enum TriggerType
{
    left,
    right,
    buttom
}

public class EdgeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject gameManager;
    [SerializeField] TriggerType type;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandlePlayerCollision(int num_keys)
    {
        var progression = gameManager.GetComponent<ProgressionManager>();
        progression.CheckLevelProgression(
            num_keys: num_keys, type: type
        );
    }
}
