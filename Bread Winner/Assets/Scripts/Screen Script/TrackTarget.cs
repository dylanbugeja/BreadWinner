using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTarget : MonoBehaviour
{
    #region instance
    public static TrackTarget instance;
    private void Awake()
    {
        camera = GetComponent<Camera>();
        camera.orthographic = true;
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] private List<Transform> targets = new List<Transform>();
    [SerializeField] private float boundingBoxPadding = 2f;
    [SerializeField] private float minimumOrthographicSize = 5f;
    [SerializeField] private float zoomSpeed = 20f;
    new Camera camera;

    void LateUpdate()
    {
        Rect boundingBox = CalculateTargetsBoundingBox();
        transform.position = CalculateCameraPosition(boundingBox);
        camera.orthographicSize = CalculateOrthographicSize(boundingBox);
    }
    private Rect CalculateTargetsBoundingBox()
    {
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float minY = Mathf.Infinity;
        float maxY = Mathf.NegativeInfinity;

        foreach (Transform target in targets)
        {
            Vector3 position = target.position;

            minX = Mathf.Min(minX, position.x);
            minY = Mathf.Min(minY, position.y);
            maxX = Mathf.Max(maxX, position.x);
            maxY = Mathf.Max(maxY, position.y);
        }

        return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, maxX + boundingBoxPadding, minY - boundingBoxPadding);
    }
    private Vector3 CalculateCameraPosition(Rect boundingBox)
    {
        Vector2 boundingBoxCenter = boundingBox.center;

        return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, camera.transform.position.z);
    }
    private float CalculateOrthographicSize(Rect boundingBox)
    {
        float orthographicSize = camera.orthographicSize;
        Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
        Vector3 topRightAsViewport = camera.WorldToViewportPoint(topRight);

        if (topRightAsViewport.x >= topRightAsViewport.y)
            orthographicSize = Mathf.Abs(boundingBox.width) / camera.aspect / 2f;
        else
            orthographicSize = Mathf.Abs(boundingBox.height) / 2f;

        return Mathf.Clamp(Mathf.Lerp(camera.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, Mathf.Infinity);
    }
    public void AddTarget(Transform newTarget)
    {
        if (!targets.Contains(newTarget))
        {
            targets.Add(newTarget);
        }
    }
    public void RemoveTarget(Transform targetToRemove)
    {
        if (targets.Contains(targetToRemove))
        {
            targets.Remove(targetToRemove);
        }
    }
}