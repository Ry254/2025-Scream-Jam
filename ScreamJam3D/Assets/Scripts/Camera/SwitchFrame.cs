using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFrame : MonoBehaviour, IMouseInteract
{
    public Bounds2D bounds;
    public PlayerLookState targetFrame;

    private bool _touchBuffer = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bounds = ConvertToPercentBounds(bounds);

        if (targetFrame == PlayerLookState.None)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnToggleOn()
    {
        _touchBuffer = true;
    }

    public void OnMouseMove(Vector2 mousePos)
    {
        if (bounds.minX > mousePos.x || bounds.maxX < mousePos.x || bounds.minZ > mousePos.y || bounds.maxZ < mousePos.y)
        {
            _touchBuffer = false;
        }
        else if (!_touchBuffer)
        {
            CameraManager.Instance.CurrentLookState = targetFrame;
        }
    }

    public void OnPress(Vector2 mousePos)
    {
        
    }

    public void OnDrawGizmosSelected()
    {
        if (targetFrame == PlayerLookState.None)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.white;
        }

        Bounds2D shiftedBounds = ConvertToPercentBounds(bounds);

        Vector3[] points = new Vector3[]
        {
            new(shiftedBounds.minX, shiftedBounds.minZ),
            new(shiftedBounds.minX, shiftedBounds.maxZ),
            new(shiftedBounds.maxX, shiftedBounds.maxZ),
            new(shiftedBounds.maxX, shiftedBounds.minZ)
        };


        Gizmos.DrawLine(points[0], points[1]);
        Gizmos.DrawLine(points[1], points[2]);
        Gizmos.DrawLine(points[2], points[3]);
        Gizmos.DrawLine(points[3], points[0]);
    }

    private static Bounds2D ConvertToPercentBounds(Bounds2D bounds)
    {
        float yBounds = Camera.main.pixelHeight;
        float xBounds = yBounds * Camera.main.aspect;

        return new Bounds2D(
            bounds.minX / 100 * xBounds,
            bounds.minZ / 100 * yBounds,
            bounds.maxX / 100 * xBounds,
            bounds.maxZ / 100 * yBounds);

    }

    private static Bounds2D ConvertToPixelBounds(Bounds2D bounds)
    {
        float yBounds = Camera.main.pixelHeight;
        float xBounds = yBounds * Camera.main.aspect;

        return new Bounds2D(
            bounds.minX / xBounds * 100,
            bounds.minZ / yBounds * 100,
            bounds.maxX / xBounds * 100,
            bounds.maxZ / yBounds * 100);
    }
}
