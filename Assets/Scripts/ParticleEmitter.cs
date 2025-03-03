using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    [SerializeField] private List<Particle> particles;
    [SerializeField] private Particle particlePrefab;

    [SerializeField] private Vector3 particleStartPosition;

    private void FixedUpdate()
    {
        particles.Add(Instantiate(particlePrefab, particleStartPosition, Quaternion.identity));

        for (int i = 0; i < particles.Count; i++)
        {
            Particle particle = particles[i];
            if (!particle.isAlive())
            {
                particles.RemoveAt(i);
            }
        }
    }
}
