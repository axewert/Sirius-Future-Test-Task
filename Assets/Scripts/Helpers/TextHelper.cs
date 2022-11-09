using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class TextHelper : MonoBehaviour
{
    private static readonly string alphabet = "abcdefghijklmnopqrstuvwxyz";

    public static List<string> GetUniqueWords(string text, int minWordLength)
    {
        text = new Regex("[^a-zA-Z]").Replace(text, " ");
        string[] words = text.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        return words.Distinct().ToList().FindAll(word => word.Length >= minWordLength);
    }
}

