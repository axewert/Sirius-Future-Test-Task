using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] SecretWordField secretWordField;
    private List<string> uniqueWords;
    private List<char> currentWord;

    private void Awake()
    {
        string text = Resources.Load<TextAsset>("Text/alice30").text;
        uniqueWords = TextHelper.GetUniqueWords(text, 10);
        SetCurrentWord();
    }
    private void SetCurrentWord()
    {
        int randomIndex = Random.Range(0, uniqueWords.Count);
        currentWord = uniqueWords[randomIndex].ToList();

        Debug.Log($"Current secret word: {uniqueWords[randomIndex]}");

        uniqueWords.RemoveAt(randomIndex);
        secretWordField.SetLetters(currentWord);
    }

}
