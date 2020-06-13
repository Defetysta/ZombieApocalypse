using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    private const string ENEMY_TAG = "Enemy";
    private const string BOUNDS_TRIGGER_TAG = "BoundsTrigger";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ENEMY_TAG))
        {
            gameObject.SetActive(false);
        }
        else if (other.CompareTag(BOUNDS_TRIGGER_TAG))
        {
            gameObject.SetActive(false);
        }
    }
}
