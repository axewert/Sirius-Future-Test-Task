using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    [SerializeField] KeyboardKey keyPrefab;
    [SerializeField] GameObject rowPrefab;
    [SerializeField] int rowSize;

    private List<KeyboardKey> keys = new();

    public void SetKeys(List<char> letters)
    {
        int rowCount = (int)Mathf.Ceil((float)letters.Count / (float)rowSize);
        
        for (int i = 0; i < rowCount; i++)
        {
            GameObject row = Instantiate(rowPrefab, transform);

            for (int j = 0; j < rowSize; j++)
            {
                int letterIndex = i * rowSize + j;
                if (letterIndex >= letters.Count) break;

                KeyboardKey newKey = Instantiate(keyPrefab, row.gameObject.transform);
                newKey.Set(letters[letterIndex]);
                keys.Add(newKey);
            }
        }
        
    }
}
