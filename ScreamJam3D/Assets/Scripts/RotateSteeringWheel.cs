using UnityEngine;

public class RotateSteeringWheel : MonoBehaviour
{
    private Quaternion _startingRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startingRotation = transform.localRotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.up, Color.green);
        transform.localRotation = _startingRotation;
        transform.RotateAround(transform.position, transform.up, SteeringWheelControls.Instance.AngleToVertical);
    }
}
