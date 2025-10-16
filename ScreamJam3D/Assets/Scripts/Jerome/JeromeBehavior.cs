using System.Diagnostics;
using UnityEngine;

public class JeromeBehavior : MonoBehaviour
{
    private Stopwatch sleepTimer, awakeTimer;

    //time in milliseconds
    [SerializeField]
    private int sleepTime = 10000, awakeTime = 10000;

    [SerializeField]
    private SkinnedMeshRenderer mesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sleepTimer = new Stopwatch();
        awakeTimer = new Stopwatch();

        CameraManager.Instance.OnCameraChange += Look;

        mesh.enabled = false;

        sleepTimer.Restart();
        awakeTimer.Stop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sleepTimer.ElapsedMilliseconds >= sleepTime && sleepTimer.IsRunning)
        {
            if (!CameraManager.Instance.LookingRightOfWheel)
            {
                SpawnJerome();
            }
        }

        if (awakeTimer.ElapsedMilliseconds >= awakeTime && awakeTimer.IsRunning)
        {
            DespawnJerome();
        }
    }

    private void SpawnJerome()
    {
        mesh.enabled = true;
        sleepTimer.Stop();
        awakeTimer.Restart();
        LightFlickerController.Flicker();
        LocalAudioManager.Instance.IsJeromeActive = true;
    }

    private void DespawnJerome()
    {
        mesh.enabled = false;
        awakeTimer.Stop();
        sleepTimer.Restart();
        LightFlickerController.Flicker();
        LocalAudioManager.Instance.IsJeromeActive = false;
    }

    public void Look(PlayerLookState look)
    {
        if (look == PlayerLookState.RightWindow && awakeTimer.IsRunning)
        {
            awakeTimer.Stop();
            sleepTimer.Stop();
            //DespawnJerome();
            CameraManager.Instance.IsInPlay = false;
            Invoke(nameof(GoToJeromeDeath), 0.1f);
        }
    }

    public void GoToJeromeDeath()
    {
        DeathManager.Instance.CauseDeath("Curiosity killed the cat", "Some things want to be ignored");
    }

    public void OnDestroy()
    {
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.OnCameraChange -= Look;
        }
    }
}
