using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damager : MonoBehaviour
{
    [SerializeField] private int _damage;
       
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var destructible = collision.gameObject.GetComponent<Destructible>();
        if (destructible == null)
            return;

        destructible.TakeDamage(_damage);       
    }
}
