using UnityEngine;

public class SetUpSineWave : MonoBehaviour
{
    [SerializeField] private GameObject sineWaveCube;
    [SerializeField] private int numCubes;
    [SerializeField] private float periodAmountPerCube;
    [SerializeField] private float amplitude;
    [SerializeField] private float distancePerCube;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numCubes = 20;
        amplitude = 5;
        periodAmountPerCube = Mathf.PI * 2 / numCubes;
        distancePerCube = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numCubes; i++)
        {
            GameObject cube = Instantiate(sineWaveCube, this.transform);
            cube.transform.localPosition = new Vector3(i * distancePerCube, 0, 0);
            cube.GetComponent<SimpleSineMovement>().setAmplitude(amplitude);
            cube.GetComponent<SimpleSineMovement>().setAngle(i * periodAmountPerCube);
        }
    }
}
