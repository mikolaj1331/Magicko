using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class BlockEditor : MonoBehaviour
{

    [SerializeField] int sizeOfGrid = 3;
    Vector2Int positionInGrid;
    
    // Update is called once per frame
    void Update()
    {
        GridSnapping();
        ChangeObjectName();
    }

    private void GridSnapping()
    {
        transform.position = new Vector3( GetPositionOnGrid().x * sizeOfGrid, 0f, GetPositionOnGrid().y * sizeOfGrid );
    }

    private void ChangeObjectName()
    {
        string position = GetPositionOnGrid().x + " , " + GetPositionOnGrid().y;
        gameObject.name = position;
    }

    public Vector2Int GetPositionOnGrid()
    {
        Vector2Int vector = new Vector2Int(
                Mathf.RoundToInt(transform.position.x / sizeOfGrid),
                Mathf.RoundToInt(transform.position.z / sizeOfGrid));
            return vector;
    }
}
