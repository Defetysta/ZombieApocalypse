using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private const float PROJECTILE_SPEED = 40f;
    private PoolingManager poolingManager;
    private string projectilesPoolTag = "PlayerProjectile";
    private GameObject newProjectile;

    private WaitUntil awaitInput = new WaitUntil(() => Input.GetMouseButton(0));
    private WaitForSeconds awaitEndOfInterval = new WaitForSeconds(0.02f);
    private void OnEnable()
    {
        StartCoroutine(AwaitPlayerInput());
    }

    private void Start()
    {
        poolingManager = FindObjectOfType<PoolingManager>();
    }
    private IEnumerator AwaitPlayerInput()
    {
        yield return awaitInput;
        FireProjectile();
        yield return awaitEndOfInterval;
        yield return AwaitPlayerInput();
    }
    private void FireProjectile()
    {
        newProjectile = poolingManager.SpawnFromPool(projectilesPoolTag, transform.position, transform.rotation);
        newProjectile.GetComponent<Rigidbody>().velocity = transform.forward * PROJECTILE_SPEED;
    }


}
