using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyType desiredEnemy;
    private int healthPoints;
    private const string PROJECTILE_TAG = "PlayerProjectile";
    private const string PLAYER_TAG = "Player";
    private int damagePerProjectile = 60;
    [SerializeField]
    private GameEventRaiser onEnemyKilled = null;
    [SerializeField]
    private GameEventRaiser onPlayerHit = null;
    private Renderer rend;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }
    private void OnEnable()
    {
        healthPoints = desiredEnemy.enemyHP;
        rend.material.color = Random.ColorHSV(0, 1, 0.6f, 0.6f, 0.6f, 0.6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PROJECTILE_TAG))
            GetDamage();
        else if (other.CompareTag(PLAYER_TAG))
        {
            onPlayerHit.RaiseEvent();
            other.GetComponent<PlayerController>().HitPlayer(desiredEnemy.enemyDamage);
            onEnemyKilled.RaiseEvent();
            gameObject.SetActive(false);
        }
    }
    private void GetDamage()
    {
        healthPoints -= damagePerProjectile;
        if (healthPoints <= 0)
        {
            onEnemyKilled.RaiseEvent();
            gameObject.SetActive(false);
        }
    }
}
