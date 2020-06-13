using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    private const string ENEMY_TAG = "Enemy";
    private const string BOUNDS_TRIGGER_TAG = "BoundsTrigger";
    [SerializeField]
    private GameEventRaiser onEnemyKilled = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ENEMY_TAG))
        {
            onEnemyKilled.RaiseEvent();
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag(BOUNDS_TRIGGER_TAG))
        {
            gameObject.SetActive(false);
        }
    }
}
