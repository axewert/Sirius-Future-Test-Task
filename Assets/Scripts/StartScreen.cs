using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] GameSave gameSave;
    [SerializeField] Button continueButton;
    private void Awake()
    {
        if (!gameSave.isEmpty)
        {
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(1);
            });
        }
    }
    public void StartNewGame()
    {
        gameSave.ResetData();
        SceneManager.LoadScene(1);
    }
}
