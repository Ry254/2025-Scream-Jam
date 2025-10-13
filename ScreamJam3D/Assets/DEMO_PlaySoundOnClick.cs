using UnityEngine;
using UnityEngine.InputSystem;

public class DEMO_PlaySoundOnClick : MonoBehaviour
{
    public AudioClip jeromeAppears;
    public AudioClip endClick;
    AudioSource _as;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _as = GetComponent<AudioSource>();
        InputSystem.actions["Attack"].performed += context =>
        {
            _as.Play();
        };
        InputSystem.actions["Attack"].canceled += context =>
        {
            _as.PlayOneShot(endClick);
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 30000) < 10)
        {
            _as.PlayOneShot(jeromeAppears);
            Debug.Log("Playing");
        }
    }
}
