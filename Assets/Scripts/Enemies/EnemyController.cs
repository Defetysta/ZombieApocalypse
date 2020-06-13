using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyType desiredEnemy;
    private int healthPoints;
    private string PROJECTILE_TAG = "PlayerProjectile";
    private int damagePerProjectile = 60;
    [SerializeField]
    private GameEventRaiser onEnemyKilled = null;

    private void OnEnable()
    {
        healthPoints = desiredEnemy.enemyHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PROJECTILE_TAG))
            GetDamage();
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
