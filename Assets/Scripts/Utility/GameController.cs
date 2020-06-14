using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Text text = null;
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
    public void OnGameOver()
    {
        TimeSpan t = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        string formatTime = string.Format("{0:D2}m:{1:D2}s",
                t.Minutes,
                t.Seconds);
        text.text = formatTime;
    }
}
