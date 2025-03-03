using Unity.VisualScripting;
using UnityEngine;

public class RandomWalker : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private float xCoordinate = 0f;
    private float incrementAmount = 0.001f;

    private Vector3 startPos = new Vector3(0, 0, 0);
    private Vector3 velocity = new Vector3(1, 0, 1);
    private Vector3 acceleration = new Vector3(1f, 0, 0);
    void Start() {
        Debug.Log("Start!");
        this.transform.position = startPos;
    }

    // Update is called once per frame
    void Update() 
    {

        // float perlinValue = Mathf.PerlinNoise(xCoordinate, 0);
        // Debug.Log(perlinValue);
        // xCoordinate += incrementAmount;
        // float newX = Unity.Mathematics.math.remap(0f, 1f, 0f, 10f, perlinValue);
        // this.transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        
        acceleration = Random.onUnitSphere; // create a random unit vector
        acceleration.y = 0;
        acceleration *= Random.Range(-3f, 3f);
        this.velocity += acceleration * Time.deltaTime;
        this.transform.position += this.velocity * Time.deltaTime;

        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     takeStep();
        // }
    }

    private void takeStep() 
    {
        int outcome = Random.Range(0, 4 /* 5 */);
        // float floatingPointOutcome = Random.Range(0f, 1f);

        if (outcome == 0) 
        {
            // move forward
            this.transform.position += new Vector3(0, 0, 1);
        }
        else if (outcome == 1 /* || outcome == 4 */) 
        {
            // move right
            this.transform.position += new Vector3(1, 0, 0);
        }
        else if (outcome == 2) 
        {
            // move left
            this.transform.position += new Vector3(-1, 0, 0);
        }
        else if (outcome == 3) 
        {
            // move back
            this.transform.position += new Vector3(0, 0, -1);
        }
    }
}
