using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private Vector3 acceleration;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float mass;
    [SerializeField] private float maxForce;
    [SerializeField] private FlowField flowField;
    [SerializeField] private SteeringPath steeringPath;

    [SerializeField] private GameObject futurePoint;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject normalPoint;
    [SerializeField] private float lookAheadDistance;

    [SerializeField] private Vehicle[] vehicles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        acceleration = Vector3.zero;
        velocity = Vector3.zero;
        maxSpeed = 0.5f;
        mass = 5f;
        maxForce = 0.1f;
        vehicles = GameObject.FindObjectsOfType<Vehicle>();
    }

    void FixedUpdate()
    {
        //Seek(MouseWorld.GetPosition());
        //Follow(this.flowField);
        //ApplyBehaviors();
        Flock();
        
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        this.transform.position += velocity;

        transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);

        acceleration = Vector3.zero;
    }

    private void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    private void ApplyBehaviors()
    {
        Vector3 seekForce = this.Seek(MouseWorld.GetPosition());
        Vector3 separateForce = this.Separate();

        seekForce.y = 0;
        separateForce.y = 0;

        separateForce *= 1.5f;
        seekForce *= 0.5f;

        this.ApplyForce(seekForce);
        this.ApplyForce(separateForce);
    }

    private void Flock()
    {
        Vector3 separationForce = this.Separate();
        Vector3 alignmentForce = this.Align();
        Vector3 cohesionForce = this.Cohere();

        separationForce.y = 0;
        alignmentForce.y = 0;
        cohesionForce.y = 0;

        separationForce *= 1.5f;
        alignmentForce *= 1.0f;
        cohesionForce *= 1.0f;

        this.ApplyForce(separationForce);
        this.ApplyForce(alignmentForce);
        this.ApplyForce(separationForce);
    }

    private Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        Vector3 desiredVelocity = direction * this.maxSpeed;

        Vector3 steeringForce = desiredVelocity - this.velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);

        // this.ApplyForce(steeringForce);
        return steeringForce;
    }

    private void Flee(Vector3 targetPosition)
    {
        Vector3 direction = transform.position - targetPosition;
        direction.Normalize();

        Vector3 desiredVelocity = direction * this.maxSpeed;

        Vector3 steeringForce = desiredVelocity - this.velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);

        this.ApplyForce(steeringForce);
    }
    
    private Vector3 Arrive(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float currentDistance = direction.magnitude;
        direction.Normalize();

        float slowDownRadius = 10f;
        float desiredSpeed = maxSpeed;
        
        if (currentDistance < slowDownRadius)
        {
            desiredSpeed = maxSpeed * (currentDistance / slowDownRadius);
        }

        Vector3 desiredVelocity = direction * desiredSpeed;

        Vector3 steeringForce = desiredVelocity - this.velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);

        return steeringForce;
    }

    private void Follow(FlowField flowField)
    {
        GridPosition vehicleLocation = flowField.getGridSystem().GetGridPosition(this.transform.position);

        if (!flowField.getGridSystem().IsValidGridPosition(vehicleLocation))
        {
            this.acceleration = Vector3.zero;
            this.velocity = Vector3.zero;
            return;
        }

        GridObjectFlowFieldArrow flowFieldArrow = flowField.getGridSystem().GetGridObject(vehicleLocation) as GridObjectFlowFieldArrow;
        float arrowRotation = flowFieldArrow.GetArrowRotation() * Mathf.Deg2Rad;

        Vector3 desiredDirection = new Vector3(Mathf.Cos(arrowRotation), 0, Mathf.Sin(arrowRotation));
        desiredDirection.Normalize();

        Vector3 desiredVelocity = desiredDirection * this.maxSpeed;

        Vector3 steeringForce = desiredVelocity - this.velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);

        this.ApplyForce(steeringForce);
    }

    private void Follow(SteeringPath path)
    {
        Vector3 futurePosition = this.velocity.normalized * this.lookAheadDistance;
        futurePosition += this.transform.position;
        this.futurePoint.transform.position = futurePosition;

        Vector3 normalPoint = Vector3.zero;
        Vector3 target = Vector3.zero;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < path.GetLineRenderer().positionCount - 1; i++)
        {
            Vector3 startPoint = path.GetLineRenderer().GetPosition(i);
            Vector3 endPoint = path.GetLineRenderer().GetPosition(i + 1);
            normalPoint = GetNormalPoint(futurePosition, startPoint, endPoint);
            this.normalPoint.transform.position = normalPoint;

            float lineSegmentLength = Vector3.Distance(startPoint, endPoint);
            float normalPointDistance = Vector3.Distance(startPoint, normalPoint) + Vector3.Distance(endPoint, normalPoint);

            if (normalPointDistance > lineSegmentLength + 0.01f)
            {
                continue;
            }

            float currentDistance = Vector3.Distance(futurePosition, normalPoint);

            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                Vector3 B = endPoint - startPoint;
                B = B.normalized * lookAheadDistance * 1.1f;
                target = B + normalPoint;
                this.normalPoint.transform.position = normalPoint;
                this.target.transform.position = target;
            }
        }
        if (closestDistance > path.GetRadius())
        {
            Seek(target);
        }
    }

    private Vector3 Separate()
    {
        float desiredSeparation = 2f;
        Vector3 separationVector = Vector3.zero;
        int count = 0;
        foreach (Vehicle other in vehicles)
        {
            if (other == this)
            {
                continue;
            }

            float distance = Vector3.Distance(other.transform.position, this.transform.position);
            if (distance < desiredSeparation)
            {
                Vector3 diffVector = this.transform.position - other.transform.position;
                diffVector.Normalize();
                diffVector *= 1 / distance;
                separationVector += diffVector;
                count++;
            }
        }

        if (count > 0)
        {
            separationVector.Normalize();
            separationVector *= maxSpeed;
            Vector3 steeringForce = separationVector - this.velocity;
            steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
            //this.ApplyForce(steeringForce);
            return steeringForce;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 Align()
    {
        float maxNeighborDistance = 10f;
        float count = 0;

        Vector3 averageVelocity = Vector3.zero;

        foreach (Vehicle boid in vehicles)
        {
            float distance = Vector3.Distance(boid.transform.position, this.transform.position);
            if (this != boid && distance < maxNeighborDistance)
            {
                averageVelocity += boid.velocity;
                count++;
            }
        }

        if (count > 0)
        {
            averageVelocity /= vehicles.Length;
            averageVelocity.Normalize();
            averageVelocity *= maxSpeed;

            Vector3 steeringForce = averageVelocity - this.velocity;
            steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
            return steeringForce;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 Cohere()
    {
        float maxNeighborDistance = 10f;
        float count = 0;

        Vector3 averagePosition = Vector3.zero;

        foreach (Vehicle boid in vehicles)
        {
            float distance = Vector3.Distance(boid.transform.position, this.transform.position);
            if (this != boid && distance < maxNeighborDistance)
            {
                averagePosition += boid.transform.position;
                count++;
            }
        }

        if (count > 0)
        {
            averagePosition /= count;
            return Seek(averagePosition);
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 GetNormalPoint(Vector3 futurePoint, Vector3 pathStart, Vector3 pathEnd)
    {
        Vector3 A = futurePoint - pathStart;
        Vector3 B = pathEnd - pathStart;

        B.Normalize();
        float dotProduct = Vector3.Dot(A, B);

        B *= dotProduct;

        Vector3 normalPoint = pathStart + B;
        return normalPoint;
    }
}
