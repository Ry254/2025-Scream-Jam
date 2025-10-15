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
    public Pedal accelerator;
    public Pedal brake;

    // Movement variables
    [Header("Driving Variables")]
    public int maxSpeed;
    public int speedPerSecond;
    public int brakingSpeed;
    public int speedDecayPerSecond;
    public int maxTurnAngle;
    public int turnAnglePerSecond;
    [Range(0, 1)]
    public float speedRequiredForMaxTurnSpeed;
    private InputAction movementInput;
    private float truckVelocity;
    private float turningValue;

    // Score values
    private float totalScore;
    private bool crashed;

    // Save the "Move" input action
    void Start()
    {
        movementInput = InputSystem.actions.FindAction("Move");
    }

    // Process input to change forward velocity and turning, then rotate and move truck
    void Update()
    {
        if (!crashed)
        {
            float movement = accelerator.Value + brake.Value;
            truckVelocity = Mathf.Clamp(movement > 0 ? truckVelocity + movement * Time.deltaTime * speedPerSecond
                : truckVelocity + movement * Time.deltaTime * brakingSpeed, 0, maxSpeed);
            if (movement == 0)
                truckVelocity = Mathf.Clamp(truckVelocity - speedDecayPerSecond * Time.deltaTime, 0, maxSpeed);
            // turningValue = Mathf.Clamp(turningValue - movement.x * Time.deltaTime * turnAnglePerSecond, -maxTurnAngle, maxTurnAngle);
            // turningValue = Mathf.Clamp(turningValue - SteeringWheelControls.Instance.AngleToVertical * Time.deltaTime * turnAnglePerSecond, -maxTurnAngle, maxTurnAngle);
            RotateTruck(turningValue * Time.deltaTime);
            MoveTruckForward(truckVelocity * Time.deltaTime);

            // Updating and showing score
            totalScore += truckVelocity * Time.deltaTime;
            scoreDisplay.text = $"Distance: {(int)totalScore}m";
        }
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
        roadParent.RotateAround(truckTransform.position, Vector3.up, turningValue * Mathf.Clamp(truckVelocity / (maxSpeed * speedRequiredForMaxTurnSpeed), 0, 1));
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Road" && !crashed)
        {
            // Crashed, game over!
            crashed = true;
            scoreDisplay.text += "\nCrashed!";
            // All of this can likely be moved into wherever the Game Over screen logic is, assuming a reference to the current game's score exists
            int oldScore = PlayerPrefs.GetInt("Score");
            if (totalScore > oldScore)
            {
                PlayerPrefs.SetInt("Score", (int)totalScore);
                PlayerPrefs.Save();
            }
            DeathManager.Instance.CauseDeath("You crashed.", "Eyes on the road!");
        }
    }
}