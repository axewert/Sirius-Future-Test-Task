using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopUpWindow : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Button button;

    public static UnityEvent OnButtonClick = new();

    private void Awake()
    {
        button.onClick.AddListener(HanleOnButtonClick);
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
    private void HanleOnButtonClick()
    {
        OnButtonClick?.Invoke();
    }
}
