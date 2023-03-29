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
    [SerializeField] private LevelLoader levelLoader;

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
        StartCoroutine(levelLoader.LoadScene(0));
    }
    public void ChangeSceneToCredits()
    {
        StartCoroutine(levelLoader.LoadScene(2));
    } 
    public void ChangeSceneToOptions()
    {
        StartCoroutine(levelLoader.LoadScene(4));
    }
    public void ExitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
    public void ChangeSceneToMainMenu()
    {
        StartCoroutine(levelLoader.LoadScene(1));
    }
    public void ChangeSceneToCharacterController()
    {
        StartCoroutine(levelLoader.LoadScene(3));
    }
}
