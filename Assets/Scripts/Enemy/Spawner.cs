using UnityEngine;
using UnityEngine.Events;

public class Spawner : ObjectPool
{
    [SerializeField] private Enemy[] _enemyTemplates;
    [SerializeField] private float _minSecondsBetweenSpawn;
    [SerializeField] private float _maxSecondsBetweenSpawn;

    public event UnityAction<int> Died;

    private float _secondsBetweenSpawn = 1.5f;
    private float _elapsedTime = 0;

    private void Awake()
    {
        Initialize(_enemyTemplates);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        

        if (_elapsedTime >= _secondsBetweenSpawn)
        {
            if (TryGetObject(out Enemy enemy))
            {
                _elapsedTime = 0;
                _secondsBetweenSpawn = Random.Range(_minSecondsBetweenSpawn, _maxSecondsBetweenSpawn);
                SetEnemy(enemy, transform.position);
            }
        }
    }

    private void SetEnemy(Enemy enemy, Vector3 spawnPoint)
    {
        enemy.Reset();
        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint;
    }

    private void OnEnable()
    {
        foreach (var enemy in _pool)
        {
            enemy.Died += OnEnemyDied;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _pool)
        {
            enemy.Died -= OnEnemyDied;
        }
    }

    private void OnEnemyDied(int value)
    {
        Died?.Invoke(value);
    }
}
