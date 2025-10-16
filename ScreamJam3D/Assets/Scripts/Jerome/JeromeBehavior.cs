using System.Diagnostics;
using UnityEngine;

public class JeromeBehavior : MonoBehaviour
{
    private Stopwatch sleepTimer, awakeTimer;

    //time in milliseconds
    [SerializeField]
    private int sleepTime = 10000, awakeTime = 10000;

    private PlayerLookState currentLook;

    private bool wait;

    [SerializeField]
    private SkinnedMeshRenderer mesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sleepTimer = new Stopwatch();
        awakeTimer = new Stopwatch();

        currentLook = CameraManager.Instance.CurrentLookState;
        CameraManager.Instance.OnCameraChange += Look;
        wait = false;

        mesh.enabled = false;

        sleepTimer.Restart();
        awakeTimer.Stop();
        UnityEngine.Debug.Log("Started");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sleepTimer.ElapsedMilliseconds >= sleepTime && sleepTimer.IsRunning)
        {
            sleepTimer.Stop();
            if (currentLook == PlayerLookState.RightWindow || currentLook == PlayerLookState.Fridge)
            {
                wait = true;
            }
            else
            {
                wait = false;
                awakeTimer.Restart();
                mesh.enabled = true;
            }
        }

        if (awakeTimer.ElapsedMilliseconds >= awakeTime && awakeTimer.IsRunning)
        {
            awakeTimer.Stop();
            sleepTimer.Restart();
            wait = false;
            mesh.enabled = false;
        }
    }

    public void Look(PlayerLookState look)
    {
        if (wait && look != PlayerLookState.RightWindow && look != PlayerLookState.Fridge)
        {
            awakeTimer.Restart();
            wait = false;
            mesh.enabled = false;
        }
        else if (look == PlayerLookState.RightWindow && awakeTimer.IsRunning)
        {
            awakeTimer.Stop();
            sleepTimer.Stop();
            wait = false;
            mesh.enabled = false;
            DeathManager.Instance.CauseDeath("Jerome was awake.", "maybe sing some lullabys next time.");
        }
        currentLook = look;
    }
}
