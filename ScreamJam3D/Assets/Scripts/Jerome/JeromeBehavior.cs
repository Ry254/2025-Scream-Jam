using System.Diagnostics;
using UnityEngine;

public class JeromeBehavior : MonoBehaviour
{
    private Stopwatch sleepTimer, awakeTimer;

    //time in milliseconds
    [SerializeField]
    private int sleepTimeMin = 10000, sleepTimeMax = 30000, awakeTimeMin = 5000, awakeTimeMax = 15000;

    private int sleepTimeActual = 0, awakeTimeActual = 0;

    [SerializeField]
    private SkinnedMeshRenderer mesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sleepTimer = new Stopwatch();
        awakeTimer = new Stopwatch();

        CameraManager.Instance.OnCameraChange += Look;

        mesh.enabled = false;
        awakeTimer.Stop();
        sleepTimer.Restart();
        sleepTimeActual = Random.Range(sleepTimeMin, sleepTimeMax);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (sleepTimer.ElapsedMilliseconds >= sleepTimeActual && sleepTimer.IsRunning)
        {
            if (!CameraManager.Instance.LookingRightOfWheel)
            {
                SpawnJerome();
            }
        }

        if (awakeTimer.ElapsedMilliseconds >= awakeTimeActual && awakeTimer.IsRunning)
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
        awakeTimeActual = Random.Range(awakeTimeMin, awakeTimeMax);
    }

    private void DespawnJerome()
    {
        mesh.enabled = false;
        awakeTimer.Stop();
        sleepTimer.Restart();
        LightFlickerController.Flicker();
        LocalAudioManager.Instance.IsJeromeActive = false;
        sleepTimeActual = Random.Range(sleepTimeMin, sleepTimeMax);
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
        CameraManager.Instance.OnCameraChange -= Look;
    }
}
