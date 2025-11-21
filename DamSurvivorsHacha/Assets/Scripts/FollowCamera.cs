using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    private Vector3 offset;
    // Cargar controles
    private Controles controles;
    // Zooms
    private float zoomMin = 0.5f;
    private float zoomMax = 2f;
    private float zoomBase = 1f;
    private float suavizadoZoom = 10f;

    public void Awake() {
        controles = new Controles();
    }

    void OnEnable() {
        controles.Enable();
    }

    void OnDisable() {
        controles.Disable();
    }

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        float scrollValue = controles.MainCamera.Zoom.ReadValue<float>();
        zoomBase -= scrollValue / suavizadoZoom;
        zoomBase = Mathf.Clamp(zoomBase, zoomMin, zoomMax);
        Vector3 zoomFinal = offset * zoomBase;
        transform.position = player.transform.position + zoomFinal;
    }
} 