using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{

    [SerializeField] private Transform gridSystemVisualSinglePrefab;
    private GridSystemVisual[,] gridSystemVisualSingleArray;
    private GridSystem gridSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridSystemVisualSingleArray = new GridSystemVisual[gridSystem.GetWidth(), gridSystem.GetHeight()];

        for (int x = 0; x < gridSystem.GetWidth(); x++)
        {
            for (int z = 0; z < gridSystem.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridSystemVisualSingleTransform = Instantiate(gridSystemVisualSinglePrefab, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);

                gridSystemVisualSingleTransform.parent = this.transform;

                gridSystemVisualSingleTransform.localScale *= gridSystem.GetCellSize();

                gridSystemVisualSingleArray[x, z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisual>();
            }
        }
    }

    public void SetGridSystem(GridSystem gridSystem)
    {
        this.gridSystem = gridSystem;
    }
}
