using UnityEngine;

public interface IMouseInteract
{

    public void OnToggleOn();
    public void OnPress(Vector2 mousePos);

    public void OnMouseMove(Vector2 mousePos);
}