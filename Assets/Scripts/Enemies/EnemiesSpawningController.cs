using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpawningController : MonoBehaviour
{
    private const int RADIUS = 25;
    private Vector3 randPos;
    private PoolingManager poolingManager;
    private string enemiesPoolTag = "Enemies";
    private float spawnInterval = 0.1f;
    [SerializeField]
    private GameEventRaiser onEnemySpawned = null;

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
            poolingManager.SpawnFromPool(enemiesPoolTag, GetRandomPositionOnCircle() * RADIUS, Quaternion.identity);
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
