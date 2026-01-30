using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int max_enemies = 10;
    [SerializeField] private List<RectTransform> enemies; // iconos UI
    [SerializeField] private List<GameObject> prefabs; // iconos UI
    [SerializeField] private RectTransform cursor;        // cursor UI
    [SerializeField] private Transform player;            // player mundo
    [SerializeField] private float spawnCooldown = 2f;
    [SerializeField] private float dragSpeed = 600f;      // UI suele necesitar valores grandes
    [SerializeField] private float iconStopDistance = 5f; // en UI píxeles
    [SerializeField] private float playerStopDistance = 20f;
    [SerializeField] private float instance_time = 1f;

    private int enemies_counter = 0;
    private bool busy = false;

    void Start()
    {
        StartCoroutine(EnemySpawnerLoop());
    }

    IEnumerator EnemySpawnerLoop()
    {
        while (enemies_counter < max_enemies)
        {
            yield return new WaitForSeconds(spawnCooldown);

            if (!busy)
                StartCoroutine(ActivateRoutine());
        }
    }

    void MoveCursorTowards(Vector3 target)
    {
        cursor.position = Vector3.MoveTowards(
            cursor.position,
            target,
            dragSpeed * Time.deltaTime
        );
    }

    IEnumerator ActivateRoutine()
    {
        busy = true;

        int index = Random.Range(0, enemies.Count);

        // 1) Ir al ícono (UI)
        Vector3 iconPos = enemies[index].position;

        while (Vector3.Distance(cursor.position, iconPos) > iconStopDistance)
        {
            MoveCursorTowards(iconPos);
            yield return null;
        }


        yield return new WaitForSeconds(2);

        // 2) Ir hacia el jugador (convertir mundo -> pantalla)
        while (true)
        {
            Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(player.position);

            if (Vector3.Distance(cursor.position, playerScreenPos) <= playerStopDistance)
            {
                var stop_position = player.position;
                yield return new WaitForSeconds(instance_time);
                Instantiate(prefabs[index], stop_position, Quaternion.identity);
                break;
            }

            MoveCursorTowards(playerScreenPos);
            yield return null;
        }


        enemies_counter++;
        busy = false;
    }
}
