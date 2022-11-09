using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWindow : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Button button;

    private void Awake()
    {
        
    }
    public void Show(string text)
    {
        this.text.text = text;
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
