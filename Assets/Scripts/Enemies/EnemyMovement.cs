using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private static Transform player;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(player == null)
            player = FindObjectOfType<PlayerController>().transform;
    }
    private void OnEnable()
    {
        transform.LookAt(player);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (player.transform.position - transform.position).normalized * 0.3f * Time.fixedDeltaTime);
    }
}
