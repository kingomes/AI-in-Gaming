using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private Vector3 acceleration = Vector3.zero;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    [SerializeField] private GameObject ground;

    [SerializeField] private Vector3 wind;
    [SerializeField] private Vector3 gravity;

    [SerializeField] private float frictionCoefficient;
    [SerializeField] private float normal;
    [SerializeField] private float frictionMagnitude;
    [SerializeField] private Vector3 frictionDirection;
    [SerializeField] private Vector3 frictionForce;

    [SerializeField] private float G;
    [SerializeField] private float mass;
    [SerializeField] private Mover[] movers;

    [SerializeField] Vector3 angularAcceleration = Vector3.zero;
    [SerializeField]private Vector3 angularVelocity = Vector3.zero;
    [SerializeField] private Vector3 angle = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        this.mass = this.transform.lossyScale.x * this.transform.lossyScale.y * this.transform.lossyScale.z;
        
        wind = new Vector3(1, 0, 0);
        gravity = new Vector3(0, -0.1f, 0);
        gravity *= this.mass;

        G = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // if (!isInContactWithGround()) 
        // {
        //     ApplyForce(gravity);
        // }

        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            ApplyForce(wind);
        }

        if (isInContactWithGround()) 
        {
            Debug.Log("Apply friction");
            frictionCoefficient = 0.005f;
            normal = 1f;
            frictionMagnitude = frictionCoefficient * normal;

            frictionDirection = this.velocity.normalized * -1;
            frictionForce = frictionDirection * frictionMagnitude;
            ApplyForce(frictionForce);
        }

        bounceOnEdge();

        velocity += acceleration;

        this.transform.position += velocity * Time.deltaTime;

        acceleration = Vector3.zero;
    }

    public Vector3 getVelocity() 
    {
        return this.velocity;
    }

    public float getMass() 
    {
        return this.mass;
    }

    public void ApplyForce(Vector3 force) 
    {
        force /= this.mass;
        acceleration += force;
    }

    public bool isInContactWithGround() 
    {
        float myY = this.transform.position.y;
        float groundY = ground.transform.position.y;
        float myYScale = this.transform.lossyScale.y;
        float distanceThreshold = (myYScale / 2) + .1f;

        return (myY - groundY) <= distanceThreshold;
    }

    public void bounceOnEdge() 
    {
        float bounce = -0.9f;

        if (this.transform.position.y < ground.transform.position.y + this.transform.lossyScale.y / 2) {
            Debug.Log("BOUNCING!");

            this.transform.position = new Vector3(transform.position.x, ground.transform.position.y + this.transform.lossyScale.y / 2, transform.position.z);
            this.velocity.y *= bounce;
        }
    }

    public Vector3 Attract(Body mover)
    {
        Vector3 direction = this.transform.position - mover.transform.position;

        float distance = direction.magnitude;
        distance = Mathf.Clamp(distance, 5.0f, 25.0f);

        direction.Normalize();

        float magnitude = (this.G * this.mass * mover.getMass()) / (distance * distance);

        Vector3 gravitationalAccelerationForce = direction * magnitude;

        mover.ApplyForce(gravitationalAccelerationForce);

        return gravitationalAccelerationForce;
    }
}
