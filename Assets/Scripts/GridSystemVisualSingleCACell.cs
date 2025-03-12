using UnityEngine;

public class GridSystemVisualSingleCACell : GridSystemVisualSingle
{
    private GridObjectCACell gridObjectCACell;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridObjectCACell = GetGridSystem().GetGridObject(gridPosition) as GridObjectCACell;
    }

    // Update is called once per frame
    void Update()
    {
        if (gridObjectCACell != null)
        {
            if(gridObjectCACell.GetIsAlive())
            {
                this.GetMeshRenderer().material.color = Color.black;
            }
            else
            {
                this.GetMeshRenderer().material.color = Color.white;
            }
        }
    }
}
