using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public static string LoadText(string path)
    {
        return Resources.Load<TextAsset>(path).text;
    }
}
