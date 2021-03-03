using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointZoom : MonoBehaviour
{
    private void OnEnable()
    {
        TrackTarget.instance.AddTarget(transform);
    }
    private void OnDisable()
    {
        TrackTarget.instance.RemoveTarget(transform);
    }
}
