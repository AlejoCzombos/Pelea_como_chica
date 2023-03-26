using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private bool areTheCredits;

    private void Update()
    {
        if (areTheCredits)
        {
            if (Input.GetAxisRaw("Cancel") != 0)
            {
                ChangeSceneToMainMenu();
            }
        }
    }
    public void ChangeSceneToGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
    public void ChangeSceneToCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    public void ExitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
    public void ChangeSceneToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
