using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public float leftBoundary = -11f;
    public float rightBoundary = 11f;

    void Update()
    {
        if (transform.position.x > rightBoundary)
        {
            transform.position = new Vector2(leftBoundary, transform.position.y);
        }
        else if (transform.position.x < leftBoundary)
        {
            transform.position = new Vector2(rightBoundary, transform.position.y);
        }
    }
}
