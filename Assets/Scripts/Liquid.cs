using UnityEngine;
using System;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public List<Mover> movers;

    [SerializeField] private float dragCoefficient;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragCoefficient = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Mover mover in movers) 
        {
            mover.ApplyForce(CalculateDrag(mover));
        }
    }

    public Vector3 CalculateDrag(Mover mover) 
    {
        float speed = mover.getVelocity().magnitude;
        float dragMagnitude = speed * speed * dragCoefficient;

        Vector3 dragDirection = mover.getVelocity().normalized;
        dragDirection *= -1;

        Vector3 dragForce = dragDirection * dragMagnitude;

        return dragForce;
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Trigger enter");

        Mover moverComponent = other.gameObject.GetComponent<Mover>();
        if (moverComponent != null) {
            movers.Add(moverComponent);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        Mover moverComponent = other.gameObject.GetComponent<Mover>();
        if (moverComponent != null) {
            if (movers.Contains(moverComponent)) 
            {
                movers.Remove(moverComponent);
            }
        }
    }
}
