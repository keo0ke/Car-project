using UnityEngine;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private string targetName = "car 1";
    [SerializeField] private Transform target;

    [Header("Minimap Camera")]
    [SerializeField] private float height = 30f;
    [SerializeField] private float followLerpSpeed = 8f;
    [SerializeField] private float orthographicSize = 8f;
    [SerializeField] private LayerMask cullingMask = ~0;

    [Header("UI")]
    [SerializeField] private Vector2 minimapSize = new Vector2(440f, 440f);
    [SerializeField] private Vector2 topRightMargin = new Vector2(20f, 20f);

    private UnityEngine.Camera minimapCamera;
    private RawImage minimapRawImage;
    private RenderTexture minimapTexture;

    private void Start()
    {
        EnsureCanvasAndRawImage();
        EnsureMinimapCamera();
        TryFindTarget();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            TryFindTarget();
            return;
        }

        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y + height, target.position.z);
        minimapCamera.transform.position = Vector3.Lerp(
            minimapCamera.transform.position,
            desiredPosition,
            followLerpSpeed * Time.deltaTime
        );
    }

    private void TryFindTarget()
    {
        if (target != null)
        {
            return;
        }

        GameObject found = GameObject.Find(targetName);
        if (found != null)
        {
            target = found.transform;
        }
    }

    private void EnsureCanvasAndRawImage()
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("UI Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }

        Transform minimapTransform = canvas.transform.Find("Minimap");
        if (minimapTransform == null)
        {
            GameObject minimapObj = new GameObject("Minimap");
            minimapObj.transform.SetParent(canvas.transform, false);
            minimapRawImage = minimapObj.AddComponent<RawImage>();
        }
        else
        {
            minimapRawImage = minimapTransform.GetComponent<RawImage>();
            if (minimapRawImage == null)
            {
                minimapRawImage = minimapTransform.gameObject.AddComponent<RawImage>();
            }
        }

        RectTransform rect = minimapRawImage.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(1f, 1f);
        rect.anchorMax = new Vector2(1f, 1f);
        rect.pivot = new Vector2(1f, 1f);
        rect.sizeDelta = minimapSize;
        rect.anchoredPosition = new Vector2(-topRightMargin.x, -topRightMargin.y);
    }

    private void EnsureMinimapCamera()
    {
        GameObject cameraObj = GameObject.Find("MinimapCamera");
        if (cameraObj == null)
        {
            cameraObj = new GameObject("MinimapCamera");
        }

        minimapCamera = cameraObj.GetComponent<UnityEngine.Camera>();
        if (minimapCamera == null)
        {
            minimapCamera = cameraObj.AddComponent<UnityEngine.Camera>();
        }

        minimapCamera.orthographic = true;
        minimapCamera.orthographicSize = orthographicSize;
        minimapCamera.clearFlags = UnityEngine.CameraClearFlags.SolidColor;
        minimapCamera.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 1f);
        minimapCamera.cullingMask = cullingMask;
        minimapCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        if (minimapTexture == null)
        {
            minimapTexture = new RenderTexture(512, 512, 16);
            minimapTexture.name = "MinimapRT";
        }

        minimapCamera.targetTexture = minimapTexture;
        minimapRawImage.texture = minimapTexture;
    }
}
