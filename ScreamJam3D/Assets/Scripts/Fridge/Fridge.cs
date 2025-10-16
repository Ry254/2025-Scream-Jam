using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField]
    private float maxMeter = 100, decaySpeed = 5, meter;

    [SerializeField]
    private Transform tempMeter;

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
        if (decayTime >= decaySpeed)
        {
            meter -= 1;
            decayTime = 0;
        }
        else
        {
            decayTime++;
        }

        tempMeter.rotation = Quaternion.Euler(-((meter / maxMeter) * 90) + 90, 0, 0);

        if (meter == 0)
        {
            DeathManager.Instance.CauseDeath("The freezer got too hot!", "Perhaps a bit too toasty...");
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
