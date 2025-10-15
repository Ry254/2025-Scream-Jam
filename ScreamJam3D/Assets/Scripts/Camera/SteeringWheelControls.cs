using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class SteeringWheelControls : MonoBehaviour, IMouseInteract
{
    [SerializeField]
    private WheelBounds bounds;

    [SerializeField] private float _resetTime;

    [Serializable]
    private struct WheelBounds
    {
        public Vector2 Center;
        public float innerRadius;
        public float outerRadius;

        public readonly bool Contains(Vector2 position, out Vector2 delta)
        {
            delta = Center - position;
            float distanceTo = delta.magnitude;
            return distanceTo < outerRadius && distanceTo > innerRadius;
        }

        public readonly override string ToString()
        {
            return $"{{Center: ({Center.x}, {Center.y}) | Max Distance: {outerRadius} | Min Distance: {innerRadius}}}";
        }
    }
    
    // Lazy Singleton
    [HideInInspector]
    public static SteeringWheelControls Instance;
    void Awake() => Instance ??= this;

    private float originalDirection;
    
    /// <summary>
    /// Is the player dragging on the steering wheel with the intent of rotating it
    /// </summary>
    public bool IsWheelMoving { get; private set; }

    /// <summary>
    /// Negative angle = ccw motion; Positive angle = cw motion
    /// </summary>
    public float AngleToVertical { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputSystem.actions["Attack"].canceled += context => ReleaseWheel();

        bounds.Center = new Bounds2D(0, 0, Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight).Size / 2;
    }

    public void OnPress(Vector2 mousePos)
    {
        if (!bounds.Contains(mousePos, out Vector2 direction)) return;

        StopCoroutine(ResetRotation());
        IsWheelMoving = true;
        originalDirection = GetAngleDeg(direction);
    }

    public void Update()
    {
        Debug.Log(AngleToVertical);
    }

    public void OnToggleOn()
    {
        ReleaseWheel();
    }

    public void OnMouseMove(Vector2 mousePos)
    {
        if (!IsWheelMoving)
            return;

        float newAngle = GetAngleDeg(bounds.Center - mousePos);
        float deltaAngle = originalDirection - newAngle;

        if (deltaAngle <= -180)
        {
            deltaAngle += 360;
        }

        AngleToVertical = deltaAngle;
    }

    public void ReleaseWheel()
    {
        originalDirection = 0f;
        IsWheelMoving = false;

        StartCoroutine(ResetRotation());
    }

    public IEnumerator ResetRotation()
    {
        float timer = 0;
        float startValue = AngleToVertical;

        while (timer < _resetTime)
        {
            timer += Time.deltaTime;

            AngleToVertical = Mathf.Lerp(startValue, 0, timer / _resetTime);

            yield return new WaitForEndOfFrame();
        }

        AngleToVertical = 0f;
    }

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(bounds.Center, bounds.outerRadius);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(bounds.Center, bounds.innerRadius);
    }
#endif

    protected float GetAngleDeg(Vector2 directionVector) => Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
}
