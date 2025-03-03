using UnityEngine;

public class FlowField : MonoBehaviour
{
    private GridSystem gridSystem;
    [SerializeField] private GridSystemVisual gridSystemVisual;
    [SerializeField] private Transform gridDebugObjectPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        gridSystem = new GridSystem(20, 20, 2, this.transform.position, gridSystemVisual, InstantiateFlowFieldObject);
        gridSystemVisual.SetGridSystem(gridSystem);

        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        gridSystem.SetOrigin(this.transform.position);
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomizeFlowFieldArrows();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PerlinNoiseFlowFieldArrows();
        }
        // gridSystem.drawDebugLines();
    }

    private GridObject InstantiateFlowFieldObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        float arrowRotation = Random.Range(0f, 360f);
        return new GridObjectFlowFieldArrow(gridSystem, gridPosition, arrowRotation);
    }

    private void RandomizeFlowFieldArrows()
    {
        for (int x = 0; x < gridSystem.GetWidth(); x++)
        {
            for (int z = 0; z < gridSystem.GetHeight(); z++)
            {
                GridObjectFlowFieldArrow arrowObject = gridSystem.GetGridObject(x, z) as GridObjectFlowFieldArrow;
                arrowObject.SetArrowRotation(Random.Range(0f, 360f));
            }
        }
    }

    private void PerlinNoiseFlowFieldArrows()
    {
        float incrementAmount = 0.01f;
        float xCoordinate = Random.Range(0f, 0.5f);
        float zCoordinate = Random.Range(0f, 0.5f);

        for (int x = 0; x < gridSystem.GetWidth(); x++)
        {
            for (int z = 0; x < gridSystem.GetHeight(); z++)
            {
                float perlinValue = Mathf.PerlinNoise(xCoordinate, zCoordinate);

                float arrowRotation = Unity.Mathematics.math.remap(0f, 1f, 0f, 360f, perlinValue);
                GridObjectFlowFieldArrow arrowObject = gridSystem.GetGridObject(x, z) as GridObjectFlowFieldArrow;
                arrowObject.SetArrowRotation(arrowRotation);
                zCoordinate += incrementAmount;
            }
            xCoordinate += incrementAmount;
        }
    }

    public GridSystem getGridSystem()
    {
        return this.gridSystem;
    }
}
