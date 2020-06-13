using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private IntVariable maxHP = null;
    private int currentHP;

    private void OnEnable()
    {
        currentHP = maxHP.Value;
    }

    public void HitPlayer(int damage)
    {
        currentHP -= damage;
    }

    private void Start()
    {
        StartCoroutine(AwaitPlayerDying());
    }

    private void Update()
    {
       
    }
    private IEnumerator AwaitPlayerDying()
    {
        yield return new WaitUntil(() => currentHP <= 0);
        Debug.Log("Player died!");
    }
}
