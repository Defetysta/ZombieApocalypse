using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
    private Text text;
    [SerializeField]
    private IntVariable desiredValue = null;    
    private string baseText;
    private void Awake()
    {
        text = GetComponent<Text>();
        baseText = text.text;
        if (desiredValue.ResetOnStart)
            desiredValue.Value = 0;
    }
    //private void Update()
    //{
    //    text.text = baseText + desiredValue.Value.ToString();
    //}
    public void SetDesiredText()
    {
        text.text = baseText + desiredValue.Value.ToString();
    }
}
