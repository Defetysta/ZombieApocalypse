using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private static Transform player;
    private Rigidbody rb;
    private EnemyController controller;
    private float enemyMovementSpeed;
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
        rb.MovePosition(transform.position + (player.transform.position - transform.position).normalized * enemyMovementSpeed * Time.fixedDeltaTime);
    }
}
