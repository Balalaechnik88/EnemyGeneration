using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private Enemy _enemyPrefab;

    [Header("Spawn Points")]
    [SerializeField] private EnemySpawnPoint[] _spawnPoints;

    [Header("Spawn Timing")]
    [SerializeField] private float _spawnIntervalSeconds = 2f;

    private Coroutine _spawnRoutine;

    private void Awake()
    {
        if (_enemyPrefab == null)
        {
            Debug.LogError("EnemySpawner: Enemy prefab не назначен!");
        }

        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogError("EnemySpawner: массив точек спавна пуст!");
        }

        if (_spawnIntervalSeconds <= 0f)
        {
            _spawnIntervalSeconds = 2f;
        }
    }

    private void OnEnable()
    {
        if (_spawnRoutine == null && _enemyPrefab != null && _spawnPoints != null && _spawnPoints.Length > 0)
        {
            _spawnRoutine = StartCoroutine(SpawnLoop());
        }
    }

    private void OnDisable()
    {
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    private IEnumerator SpawnLoop()
    {
        var wait = new WaitForSeconds(_spawnIntervalSeconds);

        while (true)
        {
            SpawnEnemyAtRandomPoint();
            yield return wait;
        }
    }

    private void SpawnEnemyAtRandomPoint()
    {
        if (_spawnPoints == null || _spawnPoints.Length == 0 || _enemyPrefab == null)
            return;

        int index = Random.Range(0, _spawnPoints.Length);
        EnemySpawnPoint spawnPoint = _spawnPoints[index];

        Vector3 spawnPosition = spawnPoint.transform.position;
        Quaternion spawnRotation = spawnPoint.transform.rotation;

        Enemy enemyInstance = Instantiate(_enemyPrefab, spawnPosition, spawnRotation);

        Vector3 moveDirection = spawnPoint.MoveDirection;
        float speed = spawnPoint.EnemySpeed;

        enemyInstance.Initialize(moveDirection, speed);
    }
}