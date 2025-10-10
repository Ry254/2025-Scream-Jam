using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField]
    private int maxMeter = 100, decaySpeed = 5, meter;

    private int decayTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meter = maxMeter;
        decayTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (decayTime == decaySpeed)
        {
            meter -= 1;
            decayTime = 0;
        }
        else
        {
            decayTime++;
        }
    }

    public void Click(int amount)
    {
        meter += amount;
        if (meter > maxMeter)
        {
            meter = maxMeter;
        }
    } 
}
