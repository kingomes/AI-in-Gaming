using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    private GridObject gridObject;

    [SerializeField] TextMeshPro textMeshPro;

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    // Update is called once per frame
    private void Update()
    {
        if (gridObject != null)
        {
            textMeshPro.text = gridObject.ToString();
        }
    }
}
