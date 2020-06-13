using System.Collections;
using UnityEngine;

public class EnemiesSpawningController : MonoBehaviour
{

    private const int RADIUS = 25;
    private Vector3 randPos;
    private PoolingManager poolingManager;
    private string enemiesPoolTag = "Enemies";
    private void Awake()
    {
        poolingManager = FindObjectOfType<PoolingManager>();
        StartCoroutine(SpawnInterval());
    }

    private void Update()
    {
        
    }

    public float delay;
    private IEnumerator SpawnInterval()
    {
        yield return new WaitForSeconds(delay);
        poolingManager.SpawnFromPool(enemiesPoolTag, GetRandomPositionOnCircle() * RADIUS, Quaternion.identity);
        yield return SpawnInterval();
    }
    private Vector3 GetRandomPositionOnCircle()
    {
        randPos = Random.onUnitSphere;
        randPos.y = 0;
        return randPos.normalized;
    }
}
