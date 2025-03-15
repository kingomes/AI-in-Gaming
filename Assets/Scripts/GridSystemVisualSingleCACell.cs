using UnityEngine;

public class GridSystemVisualSingleCACell : GridSystemVisualSingle
{
    protected GridObjectCACell gridObjectCACell;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        gridObjectCACell = GetGridSystem().GetGridObject(gridPosition) as GridObjectCACell;
    }

    // Update is called once per frame
    void Update()
    {
        if (gridObjectCACell != null)
        {
            if(!gridObjectCACell.GetPrevious() && gridObjectCACell.GetIsAlive())
            {
                this.GetMeshRenderer().material.color = Color.blue;
            }
            else if (gridObjectCACell.GetPrevious() && !gridObjectCACell.GetIsAlive())
            {
                this.GetMeshRenderer().material.color = Color.red;
            }
            else if (gridObjectCACell.GetIsAlive())
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
