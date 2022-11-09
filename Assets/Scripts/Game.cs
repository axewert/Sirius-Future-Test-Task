using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] SecretWordField secretWordField;
    [SerializeField] Keyboard keyboard;

    private List<string> uniqueWords;
    private List<char> currentWord;

    private void Awake()
    {
        string text = Resources.Load<TextAsset>("Text/alice30").text;
        uniqueWords = TextHelper.GetUniqueWords(text, 10);
        SetCurrentWord();
        keyboard.SetKeys("abcdefghijklmnopqrstuvwxyz".ToList());
        KeyboardKey.OnClick.AddListener(HandleKeyboardKeyClick);
    }
    private void SetCurrentWord()
    {
        int randomIndex = Random.Range(0, uniqueWords.Count);
        currentWord = uniqueWords[randomIndex].ToList();

        Debug.Log($"Current secret word: {uniqueWords[randomIndex]}");

        uniqueWords.RemoveAt(randomIndex);
        secretWordField.SetLetters(currentWord);
    }
    private void HandleKeyboardKeyClick(char letter)
    {
        Debug.Log($"Pressed key: {letter}");

        if (currentWord.Contains(letter))
        {
            secretWordField.ShowLetter(letter.ToString());
            currentWord.RemoveAll(l => l == letter);
        }
        else
        {

        }
    }
}
