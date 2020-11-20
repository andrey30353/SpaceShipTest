using System;
using UniRx;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    // todo IntReactiveProperty
    [SerializeField] private int _hp;   
   
    public IReactiveProperty<int> CurrentHp { get; private set; }
      
    private void Awake()
    {
        CurrentHp = new ReactiveProperty<int>(_hp);
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        CurrentHp.Value -= damage;
        if(_hp <= 0)
        {  
            Destroy(gameObject);
        }
    }
}
