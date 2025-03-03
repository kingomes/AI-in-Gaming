//using System.Numerics;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld instance;

    [SerializeField] private LayerMask mousePlaneLayerMask;

    private void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = MouseWorld.GetPosition();
    }

    public static Vector3 GetPosition() 
    {
        // creates ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // out makes it a reference, not value
        // shoots ray
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);

        // the Vector3 point the raycast collided with
        return raycastHit.point;
    }
}
