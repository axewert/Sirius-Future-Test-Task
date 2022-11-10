using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    [SerializeField] [Range(1, 20)] int secretWordMinSize;
    [SerializeField] int attemptsCount;
    [SerializeField] string winText;
    [SerializeField] string looseText;

    public int AttemptsCount { get { return attemptsCount; } }
    public int SecretWordMinSize { get { return secretWordMinSize; } }
    public string Alphabet { get { return alphabet; } }
    public string WinText { get { return winText; } }
    public string LooseText { get { return looseText; } }

    private string alphabet = "abcdefghijklmnopqrstuvwxyz";
}
