using System.Diagnostics;
using UnityEngine;

public class JeromeBehavior : MonoBehaviour
{
    private Stopwatch sleepTimer, awakeTimer;

    //time in milliseconds
    [SerializeField]
    private int sleepTime = 1000, awakeTime = 1000;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sleepTimer = Stopwatch.StartNew();
        awakeTimer = Stopwatch.StartNew();
        
        sleepTimer.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sleepTimer.ElapsedMilliseconds >= sleepTime && sleepTimer.IsRunning)
        {
            sleepTimer.Stop();
            awakeTimer.Start();
            //enable kill switch
        }

        if (awakeTimer.ElapsedMilliseconds >= awakeTime && awakeTimer.IsRunning)
        {
            awakeTimer.Stop();
            sleepTimer.Start();
            //disable kill switch
        }
    }
}
