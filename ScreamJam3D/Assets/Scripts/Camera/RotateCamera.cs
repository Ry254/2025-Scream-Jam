using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    [Serializable]
    private struct RotationNamePair
    {
        public PlayerLookState frame;
        public Quaternion rotation;
    }

    [SerializeField]
    private List<RotationNamePair> rotations;

    public float rotateTime = 0.1f;

    private Dictionary<PlayerLookState, Quaternion> frameRotations = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var pair in rotations)
        {
            frameRotations[pair.frame] = pair.rotation;
        }

        CameraManager.Instance.OnCameraChange += Rotate;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Rotate(PlayerLookState state)
    {
        StopCoroutine(Rotation(state));
        StartCoroutine(Rotation(state));
    }
    
    public IEnumerator Rotation(PlayerLookState state)
    {
        Transform t = Camera.main.transform;
        Quaternion start = t.localRotation;
        Quaternion end = frameRotations[state];
        float timer = 0;

        while (timer < rotateTime)
        {
            t.localRotation = Quaternion.Slerp(start, end, timer / rotateTime).normalized;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        t.localRotation = end;
    }
}
