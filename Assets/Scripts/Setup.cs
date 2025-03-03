using UnityEngine;

public class Setup : MonoBehaviour
{
    // [SerializeField] private Body body1;
    // [SerializeField] private Body body2;
    [SerializeField] private Body[] bodies;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bodies = FindObjectsByType<Body>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        // computationally expensive when too many bodies
        for (int i = 0; i < bodies.Length; i++)
        {
            for (int j = 0; j < bodies.Length; j++)
            {
                if (i == j) continue;
                bodies[i].Attract(bodies[j]);
            }
        }
        // body1.Attract(body2);
        // body2.Attract(body1);
    }
}
