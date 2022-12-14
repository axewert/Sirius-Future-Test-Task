using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SecretWordField : MonoBehaviour
{
    [SerializeField] SecretWordLetter letterPrefab;

    public List<string> OpenedLetters
    {
        get { return openedLetters; }
    }
    private List<SecretWordLetter> letters = new();
    private List<string> openedLetters = new();

    public void SetLetters(List<char> secretWord)
    {
        openedLetters.Clear();

        int difference = letters.Count - secretWord.Count;

        UnityAction RefreshLetters = difference > 0
            ? RemoveLetter
            : AddLetter;

        for (int i = 0; i < Mathf.Abs(difference); i++)
        {
            RefreshLetters();
        }

        for (int i = 0; i < letters.Count; i++)
        {
            letters[i].Text = secretWord[i].ToString();
        }
    }
    public void ShowLetter(string letter)
    {
        letters.FindAll(l => l.Text == letter).ForEach(l => l.Show());
        openedLetters.Add(letter);
    }
    private void AddLetter()
    {
        SecretWordLetter newLetter = Instantiate(letterPrefab, transform);
        letters.Add(newLetter);
    }
    private void RemoveLetter()
    {
        Destroy(letters[^1].gameObject);
        letters.RemoveAt(letters.Count - 1);
    }
}