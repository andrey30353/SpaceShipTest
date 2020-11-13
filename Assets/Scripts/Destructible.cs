using System;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private int _hp;   
    public event Action<int> OnChangeHpEvent;
    public event Action OnDestroyEvent;      

    public void TakeDamage(int damage)
    {
        _hp -= damage;

        OnChangeHpEvent?.Invoke(_hp);    
        
        if(_hp <= 0)
        {
            OnDestroyEvent?.Invoke();

            Destroy(gameObject);
        }
    }
}
