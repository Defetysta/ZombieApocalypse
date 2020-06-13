using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpawningController : MonoBehaviour
{
    public Text text;
    private int count = 0;
    private const int RADIUS = 25;
    private Vector3 randPos;
    private PoolingManager poolingManager;
    private string enemiesPoolTag = "Enemies";
    private int enemiesPerSpawn = 8;
    private float spawnInterval = 0.1f;
    private void Awake()
    {
        poolingManager = FindObjectOfType<PoolingManager>();
        StartCoroutine(SpawnInterval());
    }

    private void Update()
    {
        text.text = count.ToString();
    }

    public float delay;
    private IEnumerator SpawnInterval()
    {
        yield return new WaitForSeconds(spawnInterval);
        for (int i = 0; i < enemiesPerSpawn; i++)
        {
            poolingManager.SpawnFromPool(enemiesPoolTag, GetRandomPositionOnCircle() * RADIUS, Quaternion.identity);
        }
        
        count+= enemiesPerSpawn;
        yield return SpawnInterval();
    }
    private Vector3 GetRandomPositionOnCircle()
    {
        randPos = Random.onUnitSphere;
        randPos.y = 0;
        return randPos.normalized;
    }
}
