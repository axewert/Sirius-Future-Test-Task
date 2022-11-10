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
    [SerializeField] GameSave gameSave;

    private List<string> uniqueWords;
    private List<char> currentWord;

    private int successAttempts;
    private int currentAttempts;
    private int gameScore;
    private enum EndGameType { Win, Loose }

    private void Awake()
    {
        if (gameSave.isEmpty)
        {
            NewGame();
        }
        else
        {
            LoadGame();
        }
        SetKeyboard();
        StartRound();
        KeyboardKey.OnClick.AddListener(HandleKeyboardKeyClick);
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
        SetRandomWord();
        StartRound();
    }
    private void LoadGame()
    {
        currentAttempts = gameSave.currentAttempts;
        gameScore = gameSave.gameScore;
        uniqueWords = gameSave.uniqueWords;
        currentWord = gameSave.currentWord;
        successAttempts = gameSave.successAttempts;
    }
    private void StartRound()
    {
        scoreCounter.SetCount(gameScore);
        attemptsCounter.SetCount(currentAttempts);
        secretWordField.SetLetters(currentWord);

        if (gameSave.openedLetters.Count > 0)
        {
            gameSave.openedLetters.ForEach(letter => secretWordField.ShowLetter(letter));
            keyboard.HideKeys(gameSave.openedLetters);
        }
    }
    private void SetRandomWord()
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
        currentAttempts = gameSettings.AttemptsCount;
        attemptsCounter.SetCount(currentAttempts);
        SetRandomWord();
        keyboard.ResetKeys();
        secretWordField.SetLetters(currentWord);
        gameSave.openedLetters.Clear();
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
            KeyboardKey.OnClick.AddListener(HandleKeyboardKeyClick);
        });
        gameSave.ResetData();
    }
    private void OnApplicationQuit()
    {
        gameSave.currentAttempts = currentAttempts;
        gameSave.gameScore = gameScore;
        gameSave.uniqueWords = uniqueWords;
        gameSave.openedLetters = secretWordField.OpenedLetters;
        gameSave.currentWord = currentWord;
        gameSave.successAttempts = successAttempts;
        gameSave.isEmpty = false;
    }
}
