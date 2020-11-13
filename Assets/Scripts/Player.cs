using System;
using UnityEngine;

[Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private float _speed;
    [SerializeField] private Boundary _boundary;

    [Space]
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletPoint;
    
    [Range(0.1f, 1)]
    [SerializeField] private float _shotTimeout;

    public event Action<int> OnChangeHpEvent;

    private Vector2 _movement;
    private float _shotTimeoutProcess;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    private void Update()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        _movement = new Vector2(moveHorizontal, moveVertical);

        _shotTimeoutProcess -= Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (_shotTimeoutProcess <= 0)
                Shot();
        }
    }

    private void FixedUpdate()
    {     
        rb.velocity = _movement * _speed;

        var newX = Mathf.Clamp(rb.position.x, _boundary.xMin, _boundary.xMax);
        var newY = Mathf.Clamp(rb.position.y, _boundary.yMin, _boundary.yMax);
        rb.position = new Vector2(newX, newY);
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;

        OnChangeHpEvent?.Invoke(_hp);        
    }

    public void Shot()
    {
        var bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = _bulletPoint.position;

        _shotTimeoutProcess = _shotTimeout;

        OnChangeHpEvent?.Invoke(_hp);
    }

    private void OnDrawGizmosSelected ()
    {
        if(_bulletPoint != null)
        {
            Gizmos.DrawWireSphere(_bulletPoint.position, 0.1f);
        }
    }
}
