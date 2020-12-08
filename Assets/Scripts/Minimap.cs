using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform playerTransform;

    void LateUpdate()
    {
        Vector3 newPos = playerTransform.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
    }
}
