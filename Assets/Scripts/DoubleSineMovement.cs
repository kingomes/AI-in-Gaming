using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DoubleSineMovement : MonoBehaviour
{
    [SerializeField] private Vector3 amplitude;
    [SerializeField] private Vector3 angle;
    [SerializeField] private Vector3 angularVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        amplitude = new Vector3(Random.Range(2, 8), 0, Random.Range(2, 8));
        angularVelocity = new Vector3(Random.Range(-0.005f, 0.005f), 0, Random.Range(-0.005f, 0.005f));
        angle = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = amplitude.x * Mathf.Sin(angle.x);
        float newZ = amplitude.z * Mathf.Sign(angle.z);

        this.transform.position = new Vector3(newX, this.transform.position.y, newZ);

        angle.x += angularVelocity.x;
        angle.z += angularVelocity.z;
        // sineInput =+= incrementSpeed * Time.deltaTime;
    }
}
