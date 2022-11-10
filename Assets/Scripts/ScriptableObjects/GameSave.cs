using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSave", menuName = "ScriptableObjects/GameSave", order = 2)]

public class GameSave : ScriptableObject
{
    public bool isEmpty = true;

    public List<char> currentWord;
    public int currentAttempts;
    public int successAttempts;
    public int gameScore;
    public List<string> uniqueWords;
    public List<string> openedLetters;

    public void ResetData()
    {
        currentWord.Clear();
        currentAttempts = 0;
        successAttempts = 0;
        gameScore = 0;
        uniqueWords.Clear();
        openedLetters.Clear();
        isEmpty = true;
    }
}
