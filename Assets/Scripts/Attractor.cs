using UnityEngine;

public class Attractor : MonoBehaviour
{

    [SerializeField] private float G;
    [SerializeField] private float mass;
    [SerializeField] private Mover[] movers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        G = 1.0f;
        mass = 10.0f;
        movers = FindObjectsByType<Mover>(FindObjectsSortMode.None);   
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Mover mover in movers)
        {
            Vector3 gravitationalForce = Attract(mover);
            mover.ApplyForce(gravitationalForce);
        }
    }

    public Vector3 Attract(Mover mover)
    {
        Vector3 direction = this.transform.position - mover.transform.position;

        float distance = direction.magnitude;
        distance = Mathf.Clamp(distance, 5.0f, 25.0f);

        direction.Normalize();

        float magnitude = (this.G * this.mass * mover.getMass()) / (distance * distance);

        Vector3 gravitationalAccelerationForce = direction * magnitude;

        return gravitationalAccelerationForce;
    }
}
