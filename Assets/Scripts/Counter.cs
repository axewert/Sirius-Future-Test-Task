using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] TMP_Text count;

    public void SetCount(int value)
    {
        if (value < 0) return;
        count.text = value.ToString();
    }
}
