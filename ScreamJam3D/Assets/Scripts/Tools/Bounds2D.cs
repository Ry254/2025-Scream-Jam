using UnityEngine;

[System.Serializable]
public struct Bounds2D
{
    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

    public Vector2 Min => new Vector2(minX, minZ);
    public Vector2 Max => new Vector2(maxX, maxZ);
    public Vector2 Size => Max - Min;

    public Bounds2D(float _minX, float _minZ, float _maxX, float _maxZ)
    {
        minX = _minX;
        minZ = _minZ;
        maxX = _maxX;
        maxZ = _maxZ;
    }

    public override string ToString() => $"Min X: {minX}, Max X: {maxX}, Min Z: {minZ}, Max Z: {maxZ}";

}
