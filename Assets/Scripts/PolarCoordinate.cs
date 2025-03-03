using UnityEngine;

public class PolarCoordinate : MonoBehaviour
{
    [SerializeField] private float r;

    [SerializeField] private float theta;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        r = 5.45f;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = r * Mathf.Cos(theta);
        float newZ = r * Mathf.Sin(theta);

        Vector3 newPos = new Vector3(newX, transform.position.y, newZ);
        transform.position = newPos;

        theta += 0.002f;
    }
}
