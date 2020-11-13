using UnityEngine;

public enum Direction
{
    Up,
    Down
}

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Direction _direction;
    [SerializeField] private float _speed;   
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        rb.velocity = DefineDirection(_direction) * _speed;
    }   

    private Vector2 DefineDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector2.up;
              
            case Direction.Down:
                return Vector2.down;

            default:
                throw new System.NotImplementedException($"Unknown direction = {direction}");
        }
    }
}
