using UnityEngine;

public class SimpleSineMovement : MonoBehaviour
{
    [SerializeField] private float sineOutput;
    [SerializeField] private float amplitude;
    // [SerializeField] private float period;

    [SerializeField] private float angle;
    [SerializeField] private float angularVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        amplitude = 5;
        // period = 600;
        angularVelocity = 0.005f;
    }

    // Update is called once per frame
    void Update()
    {
        sineOutput = amplitude * Mathf.Sin(angle /*Mathf.PI * 2 * Time.frameCount / period*/);

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, sineOutput);

        angle += angularVelocity;
        // sineInput =+= incrementSpeed * Time.deltaTime;
    }

    public void setAngle(float angle)
    {
        this.angle = angle;
    }

    public void setAmplitude(float apmplitude)
    {
        this.amplitude = apmplitude;
    }
}
