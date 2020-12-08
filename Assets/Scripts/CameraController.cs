using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 target_Offset;
    public float smoothValue = 0.1f;
    private void Start()
    {
        target_Offset = transform.position - target.position;
    }

    void Update()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + target_Offset, smoothValue);
        }
    }
}