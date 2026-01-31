using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject layerIcon;     // <-- cambia a RectTransform
    [SerializeField] GameObject eyeObject;     // <-- cambia a RectTransform
    [SerializeField] Canvas canvas;
               // <-- referencia al Canvas

    private AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public void HandlePlayerCollision(int num_keys)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        StartCoroutine(KeyPickupRoutine(num_keys));
    }

    IEnumerator KeyPickupRoutine(int num_keys)
    {
        // 1) Mundo -> Screen (pixeles)
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // 2) Screen -> UI world (posición real para RectTransform.position)
        Camera uiCam = (canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : canvas.worldCamera;

        RectTransform canvasRT = canvas.transform as RectTransform;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvasRT,
            screenPos,
            uiCam,
            out Vector3 uiStartPos
        );

        // 3) Spawn/activar el icono volador en UI
        eyeObject.gameObject.SetActive(true);
        eyeObject.GetComponent<RectTransform>().position = uiStartPos;


        Vector3 start = eyeObject.GetComponent<RectTransform>().position;       // UI world
        Vector3 end = layerIcon.GetComponent<RectTransform>().position;         // UI world (mismo espacio)

        float t = 0f;
        float duration = 0.6f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float easedT = ease.Evaluate(t);

            eyeObject.GetComponent<RectTransform>().position = Vector3.Lerp(start, end, easedT);
            yield return null;
        }

        eyeObject.GetComponent<RectTransform>().position = end;
        eyeObject.SetActive(false);

        layerIcon.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite; 
        gameManager.GetComponent<ProgressionManager>().CheckLevelProgression(num_keys);
    
        Destroy(gameObject);
    }
}
