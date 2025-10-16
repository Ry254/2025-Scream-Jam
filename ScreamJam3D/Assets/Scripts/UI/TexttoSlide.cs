using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TexttoSlide : MonoBehaviour
{
    [SerializeField]
    private Slider slide;
    [SerializeField]
    private TMP_InputField text;

    [SerializeField]
    //private AudioMixer mixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendToSlider(string num)
    {
        slide.value = float.Parse(num);
        //mixer.SetFloat("Master", float.Parse(num));
    }

    public void SendToText(float num)
    {
        text.text = num + "";
    }
}
