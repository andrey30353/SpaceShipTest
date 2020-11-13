using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Game _game;

    [SerializeField] private GameObject _prefab;

    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    private float _timeout;
    private float _timeoutProcess;

    private float _spawnCount;   

    private void Start()
    {
        _timeoutProcess = _timeout;
    }

    private void OnEnable()
    {
        _game.OnLoadLevelEvent += UpdateSettings;
    }    

    private void OnDisable()
    {
        _game.OnLoadLevelEvent -= UpdateSettings;
    }

    private void UpdateSettings(LevelData data)
    {
        _timeout = data.AsteroidTimeout;
        _timeoutProcess = data.AsteroidTimeout;

        _spawnCount = data.AsteroidCountOnSpawn;
    }

    private void Update()
    {
        _timeoutProcess -= Time.deltaTime;

        if (_timeoutProcess <= 0)
            Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            var asteroid = Instantiate(_prefab, transform);
            asteroid.transform.position = new Vector3(Random.Range(_minX, _maxX), transform.position.y);
        }

        _timeoutProcess = _timeout;
    }
}
