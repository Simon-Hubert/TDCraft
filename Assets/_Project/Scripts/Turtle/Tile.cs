using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector2 _pos;
    bool _isOccupied = false;
    [SerializeField] SpriteRenderer _spriteRenderer;
    
    public Vector2 Pos { get => _pos; }

    public Tile(Vector2 position)
    {
        _pos = position;
    }
    public void SetPos( Vector2 position) => _pos = position;
}
