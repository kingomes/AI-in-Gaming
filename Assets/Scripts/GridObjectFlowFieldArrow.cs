using UnityEngine;

public class GridObjectFlowFieldArrow : GridObject
{
    private float arrowRotation;

    public GridObjectFlowFieldArrow(GridSystem gridSystem, GridPosition gridPosition, float arrowRotation) : base(gridSystem, gridPosition)
    {
        this.arrowRotation = arrowRotation;
    }

    public override string ToString()
    {
        return "arrow \n" +  this.arrowRotation;
    }

    public float GetArrowRotation()
    {
        return this.arrowRotation;
    }

    public void SetArrowRotation(float arrowRotation)
    {
        this.arrowRotation = arrowRotation;
    } 
}
