using UnityEngine;

public class GridObjectCACell : GridObject
{
    private bool isAlive;
    private bool previous;

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

    public bool GetPrevious()
    {
        return this.previous;
    }

    public void SetIsAlive(bool isAlive)
    {
        this.previous = this.isAlive;
        this.isAlive = isAlive;
    }
}
