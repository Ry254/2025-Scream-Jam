using UnityEngine;

public class OutlineHover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverEnter()
    {
        //outlined objects are layer 3
        gameObject.layer = 3;
    }

    public void OnHoverExit()
    {
        //normal objects are layer 0
        gameObject.layer = 0;
    }
}
