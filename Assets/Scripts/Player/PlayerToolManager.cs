using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerToolManager : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject toolPrefab;
    [SerializeField] GameObject toolInstance;
    [SerializeField] float spawnCooldown = 2f;

    public bool hasTool = false;
    private bool readyToSpawn = true;

    void Awake()
    {
        toolInstance.SetActive(false);
    }

    public void ActivateTool()
    {
        if (!hasTool)
        {
            toolInstance.SetActive(true);
            hasTool = true;
        }
    }

    public void UseTool()
    {
        if (hasTool && readyToSpawn)
        {
            Debug.Log("Using tool");
            var toolSpawn = Instantiate(
                toolPrefab,
                toolInstance.gameObject.transform.position,
                Quaternion.identity
            );
            StartCoroutine(ToolCooldown(toolSpawn));
            StartCoroutine(SpawnCooldown(toolSpawn));
        }
    }

    IEnumerator ToolCooldown(GameObject toolSpawn)
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(toolSpawn);
    }

    IEnumerator SpawnCooldown(GameObject toolSpawn)
    {
        readyToSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        readyToSpawn = true;
    }

}
