using UnityEngine;

public class MouseWalker : MonoBehaviour
{
    private Vector3 velocity = new Vector3(0, 0, 0);
    private Vector3 acceleration = new Vector3(0, 0, 0);

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = MouseWorld.instance.transform.position;

        Vector3 directionVector = mousePosition - this.transform.position;

        directionVector.Normalize();

        directionVector *= 5;

        acceleration = directionVector;

        this.velocity += acceleration * Time.deltaTime;
        this.transform.position += this.velocity * Time.deltaTime;
    }
}
