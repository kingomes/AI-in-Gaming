using System;
using Unity.Collections;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 origin;

    private GridObject[,] gridObjectArray;

    private GridSystemVisual gridSystemVisual;

    public delegate GridObject InstantiateGridObjectDelegate(GridSystem gridSystem, GridPosition gridPosition);

    public GridSystem(int width, int height, float cellSize, Vector3 origin, GridSystemVisual gridSystemVisual, InstantiateGridObjectDelegate instantiateAppropriateGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;
        this.gridSystemVisual = gridSystemVisual;

        gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < this.width; x++)
        {
            for (int z = 0; z < this.height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = instantiateAppropriateGridObject(this, gridPosition);
            }
        }
    }

    public void drawDebugLines()
    {
        for (int x = 0; x < this.width; x++)
        {
            for (int z = 0; z < this.height; z++)
            {
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * 0.2f, Color.white, 0.1f);
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize + origin;
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return GetWorldPosition(GetGridPosition(x, z));
    }

    public GridPosition GetGridPosition(int x, int z)
    {
        return gridObjectArray[x, z].getGridPosition();
    }

    public int GetWidth()
    {
        return this.width;
    }

    public int GetHeight()
    {
        return this.height;
    }
    
    public float GetCellSize()
    {
        return this.cellSize;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        Vector3 worldPositionWithOriginOffset = worldPosition - origin;
        return new GridPosition(Mathf.RoundToInt(Mathf.Floor(worldPositionWithOriginOffset.x / cellSize)), Mathf.RoundToInt(Mathf.Floor(worldPositionWithOriginOffset.z / cellSize)));
    }

    public GridObject GetGridObject(int x, int z)
    {
        return gridObjectArray[x, z];
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public GridDebugObject CreateDebugObjects(Transform debugObjectPrefab)
    {
        
    }
}
