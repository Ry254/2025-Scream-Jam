using UnityEngine;

public class CanvasSwap : MonoBehaviour
{
    [SerializeField]
    private Canvas[] canvas;

    // index of active canvas, by mainScreen should always be 0
    private int activeCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeCanvas = 0;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Canvas c in canvas)
        {
            c.enabled = false;
        }

        canvas[activeCanvas].enabled = true;
    }

    public void SwapCanvas(int newActive)
    {
        activeCanvas = newActive;
    }
}
