using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System;

public class Movement : MonoBehaviour
{
    // Fields
    [Header("Scene Values")]
    public Transform roadParent;
    public Transform truckTransform;
    public TextMeshPro scoreDisplay;
    public Transform speedDial;
    public Pedal accelerator;
    public Pedal brake;
    private Transform theCamera;

    // Movement variables
    [Header("Driving Variables")]
    public float maxSpeed;
    public float speedPerSecond;
    public float brakingSpeed;
    public float speedDecayPerSecond;
    public float maxSteeringWheelAngle;
    public int turnAnglePerSecond;
    [Range(0, 1)]
    public float speedRequiredForMaxTurnSpeed;
    private float truckVelocity;
    private float _velocityOld = 0;
    private float turningValue;

    // Score values
    public static float totalScore;
    private bool crashed;

    // Save the "Move" input action
    void Awake()
    {
        totalScore = 0;
        theCamera = Camera.allCameras[0].transform;
    }

    // Process input to change forward velocity and turning, then rotate and move truck
    void Update()
    {
        if (!crashed)
        {
            float movement = accelerator.Value + brake.Value;
            _velocityOld = truckVelocity;
            truckVelocity = Mathf.Clamp(movement > 0 ? truckVelocity + movement * Time.deltaTime * speedPerSecond
                : truckVelocity + movement * Time.deltaTime * brakingSpeed, 0, maxSpeed);
            if (movement == 0)
                truckVelocity = Mathf.Clamp(truckVelocity - speedDecayPerSecond * Time.deltaTime, 0, maxSpeed);
            turningValue = Mathf.Clamp(SteeringWheelControls.Instance.AngleToVertical / maxSteeringWheelAngle, -1, 1);
            RotateTruck(-turningValue * Time.deltaTime);
            MoveTruckForward(truckVelocity * Time.deltaTime);

            // Changeing audio volume
            if (_velocityOld != truckVelocity)
            {
                float proportion = Mathf.Clamp(truckVelocity, 0, 70) / 70f;
                LocalAudioManager.Instance.TruckAmbianceVolume = proportion;
            }
            
            // Updating and showing score
            totalScore += truckVelocity * Time.deltaTime;
            scoreDisplay.text = $"{(int)totalScore}m";
            speedDial.rotation = Quaternion.Euler(-(truckVelocity / maxSpeed * 180) + 90, 0, 0);
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
        roadParent.RotateAround(theCamera.position, Vector3.up, turningValue * Mathf.Clamp(truckVelocity / (maxSpeed * speedRequiredForMaxTurnSpeed) * turnAnglePerSecond, 0, turnAnglePerSecond));
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Road" && !crashed)
        {
            // Crashed, game over!
            crashed = true;
            scoreDisplay.text += "\nCrashed!";
            DeathManager.Instance.CauseDeath("You crashed.", "Eyes on the road!");
        }
    }
}