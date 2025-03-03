using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] Vector3 angleAcceleration;
    [SerializeField]private Vector3 angleVelocity;
    [SerializeField] private Vector3 angle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        angleAcceleration = new Vector3(0, 0, 0.0001f);
        angleVelocity = new Vector3(0, 0, 0);
        angle = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        angleVelocity += angleAcceleration;
        angle += angleVelocity * Time.deltaTime;
        transform.Rotate(angle);
    }
}
