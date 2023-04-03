using UnityEngine;

[System.Serializable]
public struct CrosshairState
{
    
    public Color color;
    public float size;

    public CrosshairState(Color _color, float _size) {
        color = _color;
        size = _size;
    }
}