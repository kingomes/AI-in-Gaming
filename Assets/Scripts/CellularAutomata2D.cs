using System;
using UnityEngine;

public class CellularAutomata2D : MonoBehaviour
{
    private GridSystem gridSystem;
    [SerializeField] private GridSystemVisual gridSystemVisual;

    [SerializeField] private Transform gridDebugObjectPrefab;

    private float generation;

    private void Awake()
    {
        gridSystem = new GridSystem(50, 50, 2, this.transform.position, gridSystemVisual, InstantiateCellularAutomataObject);
        gridSystemVisual.SetGridSystem(gridSystem);

        RandomizeCellStates();
        generation = 0;

        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ProduceNextGeneration();
    }

    private GridObject InstantiateCellularAutomataObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        return new GridObjectCACell(gridSystem, gridPosition, true);
    }

    private void RandomizeCellStates()
    {
        for (int x = 0; x < gridSystem.GetWidth(); x++)
        {
            for (int z = 0; z < gridSystem.GetHeight(); z++)
            {
                GridObjectCACell cell = gridSystem.GetGridObject(x, z) as GridObjectCACell;
                cell.SetIsAlive(UnityEngine.Random.Range(0, 2) == 0 ? true : false);
            }
        }
    }

    private void InitializeCellStates()
    {
        for (int x = 0; x < gridSystem.GetWidth(); x++)
        {
            GridObjectCACell cell = gridSystem.GetGridObject(x, 0) as GridObjectCACell;

            if (x == gridSystem.GetWidth() / 2)
            {
                cell.SetIsAlive(true);
            }
            else
            {
                cell.SetIsAlive(false);
            }
        }
    }

    public GridSystem getGridSystem()
    {
        return this.gridSystem;
    }

    private void ProduceNextGeneration()
    {
        bool[,] nextGeneration = getNextGenerationValues();
        GridObjectCACell cell;

        for (int x = 1; x < gridSystem.GetWidth() - 1; x++)
        {
            for (int z = 1; z < gridSystem.GetHeight() - 1; z++)
            {
                cell = gridSystem.GetGridObject(x, z) as GridObjectCACell;
                cell.SetIsAlive(nextGeneration[x, z]);
            }
        }
        generation++;
    }

    private bool RunRules(GridObjectCACell cell)
    {
        int neighborCount = GetNeighborCount(cell);

        if (cell.GetIsAlive())
        {
            if (neighborCount < 2)
            {
                return false;
            }
            else if (neighborCount > 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (neighborCount == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private bool RunRulesCave(GridObjectCACell cell)
    {
        int neighborCount = GetNeighborCount(cell);

        if (cell.GetIsAlive())
        {
            if (neighborCount < deathThreshold)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (neighborCount > birthThreshold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void jumpToGeneration(int targetGeneration)
    {
        while (generation < targetGeneration)
        {
            ProduceNextGeneration();
        }
    }

    private bool[,] getNextGenerationValues()
    {
        bool[,] nextGeneration = new bool[gridSystem.GetWidth(),gridSystem.GetHeight()];

        for (int x = 1; x < gridSystem.GetWidth() - 1; x++)
        {
            for (int z = 1; z < gridSystem.GetHeight() - 1; z++)
            {
                bool newValue = RunRules(gridSystem.GetGridObject(x, z) as GridObjectCACell);
                nextGeneration[x, z] = newValue;
            }
        }

        return nextGeneration;
    }

    private int GetNeighborCount(GridObjectCACell cell)
    {
        int neighborCount = 0;
        GridPosition gridPosition = cell.getGridPosition();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                GridObjectCACell neighbor = gridSystem.GetGridObject(gridPosition.x + i, gridPosition.z + j) as GridObjectCACell;
                neighborCount += neighbor.GetIsAlive() ? 1 : 0;
            }
        }

        neighborCount -= cell.GetIsAlive() ? 1 : 0;

        return neighborCount;
    }

    private void MakeGlider(int x, int z)
    {
        GridObjectCACell cell = gridSystem.GetGridObject(x - 1, z) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x, z - 1) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 1, z - 1) as GridObjectCACell;
        cell.SetIsAlive(true);
        
        cell = gridSystem.GetGridObject(x + 1, z) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 1, z + 1) as GridObjectCACell;
        cell.SetIsAlive(true);
    }

    private void MakeGliderGun(int x, int z)
    {
        GridObjectCACell cell = gridSystem.GetGridObject(x, z) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x, z - 1) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 1, z) as GridObjectCACell;
        cell.SetIsAlive(true);
        
        cell = gridSystem.GetGridObject(x + 1, z - 1) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 10, z) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 10, z - 1) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 10, z - 2) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 11, z + 1) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 11, z - 3) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 12, z + 2) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 12, z - 4) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 13, z + 2) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 13, z - 4) as GridObjectCACell;
        cell.SetIsAlive(true);

        cell = gridSystem.GetGridObject(x + 14, z - 1) as GridObjectCACell;
        cell.SetIsAlive(true);
    }
}
