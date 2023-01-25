using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapping : MonoBehaviour
{
    [SerializeField] BoxCollider2D GridArea;
    public Vector2Int ValidatePosition(Vector2Int position)
    {
        Bounds bounds = GridArea.bounds;

        if (position.x < bounds.min.x)
        {
            position.x = (int)bounds.max.x;
        }
        if (position.x > bounds.max.x)
        {
            position.x = (int)bounds.min.x;
        }
        if (position.y < bounds.min.y)
        {
            position.y = (int)bounds.max.y;
        }
        if (position.y > bounds.max.y)
        {
            position.y = (int)bounds.min.y;
        }

        return position;
    }
}
