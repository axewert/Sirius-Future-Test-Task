using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecretWordLetter : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public string Text
    {
        get { return text.text; }
        set
        {
            text.text = value;
            Hide();
        }
    }
    public void Show()
    {
        text.gameObject.SetActive(true);
    }
    public void Hide()
    {
        text.gameObject.SetActive(false);
    }
}
