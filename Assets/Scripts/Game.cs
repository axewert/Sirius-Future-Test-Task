using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] SecretWordField secretWordField;
    [SerializeField] Keyboard keyboard;
    [SerializeField] GameSettings gameSettings;
    [SerializeField] Counter scoreCounter;
    [SerializeField] Counter attemptsCounter;
    [SerializeField] PopUpWindow popUpWindow;

    private List<string> uniqueWords;
    private List<char> currentWord;
    private int currentAttempts;
    private int gameScore;

    private void Awake()
    {
        ResetGame();
        SetCurrentWord();
        SetKeyboard();
    }
    private void SetKeyboard()
    {
        keyboard.SetKeys(gameSettings.Alphabet.ToList());
        KeyboardKey.OnClick.AddListener(HandleKeyboardKeyClick);
    }
    private void ResetGame()
    {
        currentAttempts = gameSettings.AttemptsCount;
        //string text = Resources.Load<TextAsset>("Text/alice30").text;
        string text = "Hello world World Test";
        uniqueWords = TextHelper.GetUniqueWords(text, gameSettings.SecretWordMinSize);
        gameScore = 0;
        scoreCounter.SetCount(gameScore);
        attemptsCounter.SetCount(currentAttempts);
    }
    private void SetCurrentWord()
    {
        if (uniqueWords.Count == 0)
        {
            popUpWindow.Show(gameSettings.WinText);
            return;
        }

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
            if (currentWord.Count == 0)
            {
                StartCoroutine(NextRound());
            }
        }
        else
        {
            currentAttempts--;
            attemptsCounter.SetCount(currentAttempts);
            if (currentAttempts < 0)
            {
                popUpWindow.Show(gameSettings.LooseText);
            }
        }
    }
    private IEnumerator NextRound()
    {
        KeyboardKey.OnClick.RemoveListener(HandleKeyboardKeyClick);
        gameScore += currentAttempts;
        scoreCounter.SetCount(gameScore);
        Debug.Log($"Game Score: {gameScore}");
        SetCurrentWord();
        keyboard.ResetKeys();
        yield return new WaitForSeconds(2f);
        KeyboardKey.OnClick.AddListener(HandleKeyboardKeyClick);
    }
}
