using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Movement : MonoBehaviour
{
    // Fields
    [Header("Scene Values")]
    public Transform roadParent;
    public Transform truckTransform;
    public TextMeshPro scoreDisplay;

    // Movement variables
    private InputAction movementInput;
    private float truckVelocity;
    private float turningValue;

    // Score value
    private float totalScore;

    // Save the "Move" input action
    void Start()
    {
        movementInput = InputSystem.actions.FindAction("Move");
    }

    // Process input to change forward velocity and turning, then rotate and move truck
    void Update()
    {
        Vector2 movement = movementInput.ReadValue<Vector2>();
        truckVelocity = Mathf.Clamp(movement.y > 0 ? truckVelocity + movement.y * Time.deltaTime * 25 : truckVelocity - Time.deltaTime, 0, 50);
        turningValue = Mathf.Clamp(turningValue - movement.x * Time.deltaTime * 100, -25, 25);
        RotateTruck(turningValue * Time.deltaTime);
        MoveTruckForward(truckVelocity * Time.deltaTime);

        // Updating and showing score
        totalScore += truckVelocity * Time.deltaTime;
        scoreDisplay.text = $"Distance: {(int)totalScore}m";
    }

    /// <summary>
    /// Move the truck forward (by moving the world backwards)
    /// </summary>
    /// <param name="speedValue">How quickly to move/how far to move the world back</param>
    public void MoveTruckForward(float speedValue)
    {
        roadParent.position -= Vector3.right * speedValue;
    }

    /// <summary>
    /// Rotate the truck (by rotating the world)
    /// </summary>
    /// <param name="speedValue">How quickly to turn/How many units to turn the world</param>
    public void RotateTruck(float turningValue)
    {
        roadParent.RotateAround(truckTransform.position, Vector3.up, turningValue * truckVelocity / 25);
    }
}