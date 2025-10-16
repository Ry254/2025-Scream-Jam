using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalAudioManager : MonoBehaviour
{
    // Singleton Implementation
    public static LocalAudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("Tuck")]
    public AudioSource TruckAmbiancePlayer;
    public AudioSource TireSkidPlayer;

    [Header("Jerome")]
    public AudioSource JeromeSFXPlayer;
    public AudioClip JeromeSpawningSFX;

    private bool _isJeromeActive = false;
    
    public bool IsJeromeActive
    {
        get => _isJeromeActive;
        set
        {
            if (value == true && _isJeromeActive == false)
            {
                JeromeSFXPlayer.Play();
                JeromeSFXPlayer.PlayOneShot(JeromeSpawningSFX);
            }
            else if (value == false && _isJeromeActive == true)
            {
                JeromeSFXPlayer.Stop();
            }

            _isJeromeActive = value;
        }
    }

    public float TruckAmbianceVolume
    {
        set
        {
            TruckAmbiancePlayer.volume = value;
        }
    }

    public float SkidSoundVolume
    {
        set
        {
            TireSkidPlayer.volume = value;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CameraManager.Instance.CurrentLookState = PlayerLookState.SteeringWheel;
        CameraManager.Instance.IsInPlay = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
