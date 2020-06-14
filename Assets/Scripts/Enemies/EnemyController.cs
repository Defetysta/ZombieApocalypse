using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyType desiredEnemy;
    private int healthPoints;
    private const string PROJECTILE_TAG = "PlayerProjectile";
    private const string PLAYER_TAG = "Player";
    private const string SPELL_TAG = "Spell";
    private int damagePerBullet = 60;
    [SerializeField]
    private GameEventRaiser onEnemyKilled = null;
    [SerializeField]
    private GameEventRaiser onPlayerHit = null;
    private Renderer rend;
    private EnemyMovement movementController = null;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
        movementController = GetComponent<EnemyMovement>();
    }
    private void OnEnable()
    {
        healthPoints = desiredEnemy.enemyHP;
        rend.material.color = Random.ColorHSV(0, 1, 0.6f, 0.6f, 0.6f, 0.6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PROJECTILE_TAG))
            GetDamage(damagePerBullet);
        else if (other.CompareTag(SPELL_TAG))
        {
            switch (other.GetComponent<Spell>().spellName)
            {
                case "FireStrike":
                    GetDamage(other.GetComponent<FireStrike>().damage);
                    break;
                case "IceBlast":
                    var iceBlast = other.GetComponent<IceBlast>();
                    movementController.ApplyMovementSpeedMultiplier(iceBlast.speedMultiplier, iceBlast.duration);
                    break;
                default:
                    Debug.LogError("Unknown spell name");
                    break;
            }
        }
        else if (other.CompareTag(PLAYER_TAG))
        {
            onPlayerHit.RaiseEvent();
            other.GetComponent<PlayerController>().HitPlayer(desiredEnemy.enemyDamage);
            onEnemyKilled.RaiseEvent();
            gameObject.SetActive(false);
        }
    }
    private void GetDamage(int value)
    {
        healthPoints -= value;
        if (healthPoints <= 0)
        {
            onEnemyKilled.RaiseEvent();
            gameObject.SetActive(false);
        }
    }

}
