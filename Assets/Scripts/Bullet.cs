using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;    

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var asteroid = collision.gameObject.GetComponent<Asteroid>();
        if (asteroid == null)
            return;

        // todo
        Destroy(asteroid.gameObject);

        Destroy(gameObject);
       //asteroid.TakeDamage(_damage);
    }
}