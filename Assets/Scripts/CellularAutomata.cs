using System;
using UnityEngine;

public class CellularAutomata : MonoBehaviour
{
    private GridSystem gridSystem;
    [SerializeField] private GridSystemVisual gridSystemVisual;

    [SerializeField] private Transform gridDebugObjectPrefab;

    private float generation;

    private int[] ruleSet;

    private void Awake()
    {
        gridSystem = new GridSystem(17, 1, 2, this.transform.position, gridSystemVisual, InstantiateCellularAutomataObject);
        gridSystemVisual.SetGridSystem(gridSystem);

        InitializeCellStates();
        generation = 0;

        ruleSet = new int[8]{0, 1, 0, 1, 1, 0, 1, 0}; // ruleset 90 sierpinsky triangle
        // ruleSet = new int[8]{1, 1, 0, 1, 1, 1, 1, 0}; // ruleset 222 big triangle
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        gridSystem.SetOrigin(this.transform.position);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomizeCellStates();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ProduceNextGeneration();
        }
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
        bool[] nextGeneration = new bool[gridSystem.GetWidth()];

        for (int x = 1; x < nextGeneration.Length - 1; x++)
        {
            GridObjectCACell leftCell = gridSystem.GetGridObject(x - 1, 0) as GridObjectCACell;
            GridObjectCACell middleCell = gridSystem.GetGridObject(x, 0) as GridObjectCACell;
            GridObjectCACell rightCell = gridSystem.GetGridObject(x + 1, 0) as GridObjectCACell;

            bool newValue = RunRules(leftCell, middleCell, rightCell);
            nextGeneration[x] = newValue;
        }

        for (int x = 1; x < nextGeneration.Length - 1; x++)
        {
            Debug.Log("About to set index " + x + " to " + nextGeneration[x]);
            GridObjectCACell cell = gridSystem.GetGridObject(x, 0) as GridObjectCACell;
            cell.SetIsAlive(nextGeneration[x]);
        }

        generation++;
    }

    private bool RunRules(GridObjectCACell left, GridObjectCACell middle, GridObjectCACell right)
    {
        int leftAlive = left.GetIsAlive() ? 1 : 0;
        int middleAlive = middle.GetIsAlive() ? 1 : 0;
        int rightAlive = right.GetIsAlive() ? 1 : 0;

        string binaryString = "" + leftAlive + middleAlive + rightAlive;
        int binaryRepresentation = Convert.ToInt32(binaryString, 2);
        int ruleIndex = 7 - binaryRepresentation;

        Debug.Log("rule index is " + ruleIndex);
        int newValue = ruleSet[ruleIndex];
        bool isAlive = newValue == 1;
        return isAlive;
    }

    public void jumpToGeneration(int targetGeneration)
    {
        while (generation < targetGeneration)
        {
            ProduceNextGeneration();
        }
    }
}
