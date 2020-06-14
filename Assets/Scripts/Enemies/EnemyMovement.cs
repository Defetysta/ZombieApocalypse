using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private static Transform player;
    private Rigidbody rb;
    private EnemyController controller;
    private float enemyMovementSpeed;
    public float movementSpeedMultiplier = 1;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(player == null)
            player = FindObjectOfType<PlayerController>().transform;
        controller = GetComponent<EnemyController>();
        enemyMovementSpeed = controller.desiredEnemy.enemySpeed;
    }
    private void OnEnable()
    {
        transform.LookAt(player);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (player.transform.position - transform.position).normalized * enemyMovementSpeed * movementSpeedMultiplier * Time.fixedDeltaTime);
    }

    public void ApplyMovementSpeedMultiplier(float value, float duration)
    {
        StopAllCoroutines();
        movementSpeedMultiplier = value;
        StartCoroutine(ResetMovementSpeedMultiplier(duration));
    }

    private IEnumerator ResetMovementSpeedMultiplier(float duration)
    {
        yield return new WaitForSeconds(duration);
        movementSpeedMultiplier = 1;
    }
}
