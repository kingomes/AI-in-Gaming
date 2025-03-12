using UnityEngine;

public class GridObjectCACell : GridObject
{
    private bool isAlive;

    public GridObjectCACell(GridSystem gridSystem, GridPosition gridPosition, bool isAlive) : base(gridSystem, gridPosition)
    {
        this.isAlive = isAlive;
    }

    public override string ToString()
    {
        return "alive: \n" + isAlive;
    }

    public bool GetIsAlive()
    {
        return this.isAlive;
    }

    public void SetIsAlive(bool isAlive)
    {
        this.isAlive = isAlive;
    }
}
