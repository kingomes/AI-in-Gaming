using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private float r;
    [SerializeField] private float angle;
    [SerializeField] private float angularVelocity;
    [SerializeField] private float angularAcceleration;
    [SerializeField] private float gravity;

    [SerializeField] private GameObject bobPrefab;
    [SerializeField] private GameObject bob;

    [SerializeField] private LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravity = -0.02f;
        this.lineRenderer = GetComponent<LineRenderer>();
        bob = Instantiate(bobPrefab, this.transform.position, Quaternion.identity);
    }

    void FixedUpdate()
    {
        this.angularAcceleration = -1 * (gravity * Mathf.Sin(this.angle)) / this.r;
        this.angularVelocity += this.angularAcceleration;
        this.angle += angularVelocity;

        bob.transform.position = new Vector3(this.r * Mathf.Sin(this.angle), this.r * Mathf.Cos(this.angle), 0);

        bob.transform.position += this.transform.position;

        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, this.transform.position);
    }
}
