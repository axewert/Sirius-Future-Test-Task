using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeyboardKey : MonoBehaviour
{
    public static UnityEvent<char> OnClick = new();

    private TMP_Text text;
    private Button button;
    private char letter;

    public void Set(char letter)
    {
        text.text = letter.ToString();
        this.letter = letter;
    }

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(HandleClick);
        text = GetComponentInChildren<TMP_Text>();
    }
    private void HandleClick()
    {
        OnClick?.Invoke(letter);
        button.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
}
