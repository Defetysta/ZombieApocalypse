using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpawningController : MonoBehaviour
{
    private const int RADIUS = 25;
    private Vector3 randPos;
    private PoolingManager poolingManager;
    private float spawnInterval;
    [SerializeField]
    private EnemyType desiredEnemy = null;
    [SerializeField]
    private GameEventRaiser onEnemySpawned = null;
    private GameObject newEnemy = null;
    private int enemiesPerSpawn;
    [SerializeField]
    private IntVariable difficultyLevel = null;
    private WaitForSeconds waitForIntervalDuration;

    private void Awake()
    {
        poolingManager = FindObjectOfType<PoolingManager>();
        StartCoroutine(SpawnInterval());
        switch (difficultyLevel.Value)
        {
            case 0:
                enemiesPerSpawn = 3;
                spawnInterval = 0.02f;
                break;
            case 1:
                enemiesPerSpawn = 6;
                spawnInterval = 0.01f;
                break;
            case 2:
                enemiesPerSpawn = 8;
                spawnInterval = 0.01f;
                break;
            default:
                Debug.LogError("Wrong value of difficultyLevel");
                break;
        }
        waitForIntervalDuration = new WaitForSeconds(spawnInterval);
    }
    private IEnumerator SpawnInterval()
    {
        yield return waitForIntervalDuration;
        for (int i = 0; i < enemiesPerSpawn; i++)
        {
            newEnemy = poolingManager.SpawnFromPool(desiredEnemy.enemyTag, GetRandomPositionOnCircle() * RADIUS, Quaternion.identity);
            newEnemy.GetComponent<EnemyController>().desiredEnemy = desiredEnemy;
            onEnemySpawned.RaiseEvent();
        }
        yield return SpawnInterval();
    }
    private Vector3 GetRandomPositionOnCircle()
    {
        randPos = Random.onUnitSphere;
        randPos.y = 0;
        return randPos.normalized;
    }

}
