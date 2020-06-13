using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpawningController : MonoBehaviour
{
    private const int RADIUS = 25;
    private Vector3 randPos;
    private PoolingManager poolingManager;
    private float spawnInterval = 0.1f;
    [SerializeField]
    private EnemyType desiredEnemy = null;
    [SerializeField]
    private GameEventRaiser onEnemySpawned = null;
    private GameObject newEnemy = null;
    [SerializeField]
    private IntVariable enemiesPerSpawn = null;

    private void Awake()
    {
        poolingManager = FindObjectOfType<PoolingManager>();
        StartCoroutine(SpawnInterval());
    }
    private IEnumerator SpawnInterval()
    {
        yield return new WaitForSeconds(spawnInterval);
        for (int i = 0; i < enemiesPerSpawn.Value; i++)
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
