using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(AwaitDroppingFPS());
    }
    private IEnumerator AwaitDroppingFPS()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => 1.0f / Time.deltaTime < 30);
        Time.fixedDeltaTime = 0.03f;
    }
    private void OnDisable()
    {
        Time.fixedDeltaTime = 0.01f;
    }
}
