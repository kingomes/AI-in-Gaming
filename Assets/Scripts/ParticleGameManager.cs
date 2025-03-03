using System.Collections.Generic;
using UnityEngine;

public class ParticleGameManager : MonoBehaviour
{
    [SerializeField] private List<ParticleEmitter> emitters;
    [SerializeField] private ParticleEmitter emitterPrefab;


    void Start()
    {
                
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mouseClickPos = MouseWorld.instance.transform.position;

            ParticleEmitter emitter = Instantiate(emitterPrefab, mouseClickPos, Quaternion.identity);
            emitters.Add(emitter);
        }
    }
}
