using UnityEngine;

public class GridSystemVisualSingleFlowFieldArrow : GridSystemVisualSingle
{
    private GridObjectFlowFieldArrow gridObjectFlowFieldArrow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridObjectFlowFieldArrow = GetGridSystem().GetGridObject(gridPosition) as GridObjectFlowFieldArrow;
    }

    // Update is called once per frame
    void Update()
    {
        if (gridObjectFlowFieldArrow != null)
        {
            this.GetMeshRenderer().transform.rotation = Quaternion.Euler(90, transform.rotation.y, gridObjectFlowFieldArrow.GetArrowRotation());
        }
    }
}
