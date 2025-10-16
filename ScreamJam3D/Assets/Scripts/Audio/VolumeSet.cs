using UnityEngine;

public class VolumeSet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
