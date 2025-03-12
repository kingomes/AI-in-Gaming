using UnityEngine;

public class CAManager : MonoBehaviour
{
    [SerializeField] private CellularAutomata CAPrefab;
    [SerializeField] private int currentGeneration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGeneration = 0;
        AddLine();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddLine();
        }
    }

    private void AddLine()
    {
        CellularAutomata newCA = Instantiate(CAPrefab, transform.position + new Vector3(0, 0, -currentGeneration * 2), Quaternion.identity);
        newCA.jumpToGeneration(currentGeneration);
        currentGeneration++;
    }
}
