using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 velocity;
    private Vector3 acceleration;
    [SerializeField] private float speed;
    [SerializeField] private float maxVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = Vector3.zero;
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        speed = 0.1f;
        maxVelocity = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = MouseWorld.instance.transform.position;
        Vector3 directionVector = mousePosition - this.transform.position;
        directionVector.Normalize();
        directionVector *= speed;
        acceleration = directionVector;

        this.velocity += acceleration;
        this.velocity = Vector3.ClampMagnitude(this.velocity, maxVelocity);

        this.transform.position += this.velocity * Time.deltaTime;

        float angle = Mathf.Atan2(this.velocity.x, this.velocity.z) * Mathf.Rad2Deg;
        Debug.Log("angle is " + angle);
        Vector3 angleVector = new Vector3(0, angle, 0);
        transform.rotation = Quaternion.Euler(angleVector);

        acceleration = Vector3.zero;
    }
}
