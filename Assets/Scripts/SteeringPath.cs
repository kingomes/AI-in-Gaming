using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]

public class SteeringPath : MonoBehaviour
{
    [SerializeField] private Vector3[] points;
    [SerializeField] private Vector3 start;
    [SerializeField] private Vector3 end;
    [SerializeField] private float width;
    [SerializeField] private float radius;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject startEdge1;
    [SerializeField] private GameObject startEdge2;

    [SerializeField] private float angle;
    [SerializeField] private float angleIncrement;
    [SerializeField] private float[] amplitudes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start = transform.position;
        end = start + new Vector3(200, 0, 0);
        width = 2;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = radius;
        radius = width / 2;
        startEdge1.transform.position = this.transform.position + new Vector3(0, 0, -radius);
        startEdge2.transform.position = this.transform.position + new Vector3(0, 0, -radius);

        lineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }

        amplitudes = new float[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            amplitudes[i] = Random.Range(1f, 5f);
        }

        angle = 0;
        angleIncrement = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.widthMultiplier = radius;
    }

    void FixedUpdate()
    {
        lineRenderer.widthMultiplier = width;
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i , new Vector3(points[i].x, points[i].y, points[i].z + (Mathf.Sin(angle) * amplitudes[i])));
        }
        angle += angleIncrement;
    }

    public LineRenderer GetLineRenderer()
    {
        return this.lineRenderer;
    }

    public float GetRadius()
    {
        return this.radius;
    }
}
