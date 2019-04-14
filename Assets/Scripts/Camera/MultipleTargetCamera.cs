using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public static MultipleTargetCamera Instance { get; private set; }

    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float smoothTime = 0.5f;
    [SerializeField]
    private float minZoom = 40f;
    [SerializeField]
    private float maxZoom = 10f;
    [SerializeField]
    private float maxZoomAlone = 20f;
    [SerializeField]
    private float zoomLimiter = 50f;

    [Header("Setup")]
    [SerializeField]
    private Camera cam;

    // ---- INTER ----
    private List<Transform> listTarget = new List<Transform>();
    private Vector3 velocity;

    void Start()
    {
        Instance = this;
    }

    void LateUpdate()
    {
        if (listTarget.Count > 0)
        {
            Move();
            Zoom();
        }
    }

    public void AddTarget(Transform t)
    {
        listTarget.Add(t);
    }

    public void RemoveTarget(Transform t)
    {
        if(listTarget.Contains(t))
        listTarget.Remove(t);
    }


    private void Zoom()
    {
        if (listTarget.Count > 1)
        {
            float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreaterDistanceBetweenTargets() / zoomLimiter);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, maxZoomAlone, Time.deltaTime);
        }
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private float GetGreaterDistanceBetweenTargets()
    {
        Bounds bounds = new Bounds(listTarget[0].position, Vector3.zero);

        foreach(Transform t in listTarget)
        {
            bounds.Encapsulate(t.position);
        }

        return bounds.size.x;
    }

    private Vector3 GetCenterPoint()
    {
        if (listTarget.Count == 1)
        {
            return listTarget[0].position;
        }

        Bounds bounds = new Bounds(listTarget[0].position, Vector3.zero);
        foreach (Transform t in listTarget)
        {
            bounds.Encapsulate(t.position);
        }

        return bounds.center;
    }
}
