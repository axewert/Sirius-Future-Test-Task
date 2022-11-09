using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private void Awake()
    {
        string text = Resources.Load<TextAsset>("Text/alice30").text;
        List<string> uniqueWords = TextHelper.GetUniqueWords(text, 10);
    }
}
