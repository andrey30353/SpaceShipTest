﻿using System;
using UniRx;
using UnityEngine;

[Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Destructible))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Boundary _boundary;

    [Space]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletPoint;

    [Range(0.1f, 1)]
    [SerializeField] private float _shotTimeout;

    public event Action<int> OnChangeHpEvent;

    public IReactiveProperty<int> CurrentHp => _destructible.Hp;

    private Vector2 _movement;
    private float _shotTimeoutProcess;

    private Rigidbody2D _rb;
    private Destructible _destructible;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _destructible = GetComponent<Destructible>();
    }

    private void Start()
    {
        Observable.EveryUpdate()
            .Select(_ => new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")))
            .Subscribe(inp => _movement = inp)
            .AddTo(this);

        Observable.EveryFixedUpdate()
            .Subscribe((t) =>
            {
                _rb.velocity = _movement * _speed;

                var newX = Mathf.Clamp(_rb.position.x, _boundary.xMin, _boundary.xMax);
                var newY = Mathf.Clamp(_rb.position.y, _boundary.yMin, _boundary.yMax);

                _rb.position = new Vector2(newX, newY);
            })
            .AddTo(this);      
    }

    private void Update()
    {
        _shotTimeoutProcess -= Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (_shotTimeoutProcess <= 0)
                Shot();
        }
    }

    public void Shot()
    {
        var bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = _bulletPoint.position;

        _shotTimeoutProcess = _shotTimeout;
    }

    private void OnDrawGizmosSelected()
    {
        if (_bulletPoint != null)
        {
            Gizmos.DrawWireSphere(_bulletPoint.position, 0.1f);
        }
    }
}
