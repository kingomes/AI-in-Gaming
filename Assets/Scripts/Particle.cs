using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] private Vector3 acceleration;
    [SerializeField] protected Vector3 velocity;
    [SerializeField] private Vector3 gravity;

    [SerializeField] private float maxLifespan;
    [SerializeField] private float lifespan;
    [SerializeField] private float mass;

    [SerializeField] protected MeshRenderer meshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        acceleration = Vector3.zero;
        velocity = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        maxLifespan = 255.0f;
        lifespan = maxLifespan;
        this.meshRenderer = this.GetComponent<MeshRenderer>();
        gravity = new Vector3(0f, -0.1f, 0f);
        mass = 6.0f;
    }

    public void FixedUpdate()
    {
        ApplyForce(gravity);
        this.velocity += acceleration;
        this.transform.position += velocity;
        acceleration = Vector3.zero;

        Color currentColor = this.meshRenderer.material.color;
        currentColor.a = lifespan / maxLifespan;
        meshRenderer.material.color = currentColor;

        this.lifespan -= 2.0f;

        if(!isAlive())
        {
            Instantiate(this.gameObject, new Vector3(0f, 0f, 20f), this.transform.rotation);
            Destroy(this.gameObject);
        }

        acceleration = Vector3.zero;
    }

    private void ApplyForce(Vector3 force)
    {
        this.acceleration += force / this.mass;
    }

    public bool isAlive()
    {
        return this.lifespan > 0;
    }
}
