using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    private IntVariable enemiesAlive = null;
    [SerializeField]
    private IntVariable enemiesKilled = null;
    private int totalEnemies = 0;

    public void EnemyKilled()
    {
        enemiesKilled.ApplyChange(1);
        enemiesAlive.ApplyChange(-1);
    }

    public void EnemySpawned()
    {
        enemiesAlive.ApplyChange(1);
        totalEnemies += 1;
    }
}
