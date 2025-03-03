using System.Collections.Generic;
using UnityEngine;

public class Confetti : Particle
{
    [SerializeField] private Vector3 angularVelocity;
    [SerializeField] private Vector3 angle;

    [SerializeField] private List<Material> materials;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        angularVelocity = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        angle = new Vector3(Random.Range(-50.0f, 50.0f), Random.Range(-50.0f, 50.0f), Random.Range(-50.0f, 50.0f));
        velocity = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.25f, 1.0f), Random.Range(-0.5f, 0.5f));
        this.meshRenderer.material = this.materials[Random.Range(0, materials.Count)];
    }


    void FixedUpdate()
    {
        base.FixedUpdate();
        angle += angularVelocity;
        this.transform.rotation = Quaternion.Euler(angle);
    }
}
