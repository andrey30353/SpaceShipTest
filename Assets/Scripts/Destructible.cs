using UniRx;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private IntReactiveProperty _hp;

    public IntReactiveProperty Hp => _hp;

    public void TakeDamage(int damage)
    {
        _hp.Value -= damage;      
        if (_hp.Value <= 0)
        {
            _hp.Dispose();
            Destroy(gameObject);
        }
    }
}
