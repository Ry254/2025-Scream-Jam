using UnityEngine;

public class Pedal : MonoBehaviour
{
    public bool isAccelerator;
    public float Value { get; set; }

    public void ClickedPedal()
    {
        Value = isAccelerator ? 1 : -1;
    }
    
    public void ReleasedPedal()
    {
        Value = 0;
    }
}
