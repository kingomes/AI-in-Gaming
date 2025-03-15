using UnityEngine;

public class GridSystemVisualSingleCACellCaveSystem : GridSystemVisualSingleCACell
{
    [SerializeField] private GameObject cube;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (gridObjectCACell != null)
        {
            if (gridObjectCACell.GetIsAlive())
            {
                this.GetMeshRenderer().material.color = Color.black;
                cube.SetActive(true);
            }
            else
            {
                this.GetMeshRenderer().material.color = Color.white;
                cube.SetActive(false);
            }
        }
    }
}
