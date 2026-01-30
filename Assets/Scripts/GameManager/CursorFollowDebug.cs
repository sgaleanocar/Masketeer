using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;

public class CursorFollowDebug : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private RectTransform cursor;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject prefab;
    bool ya = true;
    private Vector3 UIToWorldPoint(float worldZ = 0f)
    {
        Vector3 screen = cursor.position;                 // píxeles en pantalla
        float z = Mathf.Abs(cam.transform.position.z - worldZ); // distancia cámara->plano Z del mundo
        screen.z = z;

        return cam.ScreenToWorldPoint(screen);
    }

    void Awake()
    {
    }

    void Update()
    {
        if (ya)
        {
            var position = UIToWorldPoint();
            Instantiate(prefab, new Vector3(position.x, position.y, position.z), Quaternion.identity);
        }
        ya = false;
    }
}