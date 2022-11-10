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
    private int successAttempts;
    private int currentAttempts;
    private int gameScore;
    private enum EndGameType { Win, Loose }

    private void Awake()
    {
        NewGame();
        SetKeyboard();
    }
    private void SetKeyboard()
    {
        keyboard.SetKeys(gameSettings.Alphabet.ToList());
    }
    private void NewGame()
    {
        currentAttempts = gameSettings.AttemptsCount;
        string text = DataLoader.LoadText("Text/alice30");

        uniqueWords = TextHelper.GetUniqueWords(text, gameSettings.SecretWordMinSize);
        gameScore = 0;
        scoreCounter.SetCount(gameScore);
        attemptsCounter.SetCount(currentAttempts);
        SetCurrentWord();
        KeyboardKey.OnClick.AddListener(HandleKeyboardKeyClick);
    }
    private void SetCurrentWord()
    {
        if (uniqueWords.Count == 0)
        {
            EndGame(EndGameType.Win);
            return;
        }

        int randomIndex = Random.Range(0, uniqueWords.Count);
        currentWord = uniqueWords[randomIndex].ToList();

        Debug.Log($"Current secret word: {uniqueWords[randomIndex]}");

        uniqueWords.RemoveAt(randomIndex);
        secretWordField.SetLetters(currentWord);

        successAttempts = 0;
    }
    private void HandleKeyboardKeyClick(char letter)
    {
        Debug.Log($"Pressed key: {letter}");

        if (currentWord.Contains(letter))
        {
            secretWordField.ShowLetter(letter.ToString());

            successAttempts += currentWord.FindAll(l => l == letter).Count;
            
            if (successAttempts == currentWord.Count)
            {
                NextRound();
            }
        }
        else
        {
            currentAttempts--;
            attemptsCounter.SetCount(currentAttempts);
            if (currentAttempts < 0)
            {
                EndGame(EndGameType.Loose);
            }
        }
    }
    private void NextRound()
    {
        gameScore += currentAttempts;
        scoreCounter.SetCount(gameScore);
        Debug.Log($"Game Score: {gameScore}");
        SetCurrentWord();
        keyboard.ResetKeys();
    }
    private void EndGame(EndGameType endGameType)
    {
        KeyboardKey.OnClick.RemoveListener(HandleKeyboardKeyClick);
        keyboard.ResetKeys();

        string text = endGameType == EndGameType.Win
            ? gameSettings.WinText
            : $"{gameSettings.LooseText}. Word: {string.Join("", currentWord)}";

        popUpWindow.Show(text);

        PopUpWindow.OnButtonClick.AddListener(() =>
        {
            NewGame();
            popUpWindow.Hide();
            PopUpWindow.OnButtonClick.RemoveAllListeners();
        });
    }
}
