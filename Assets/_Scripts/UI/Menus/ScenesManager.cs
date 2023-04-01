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

    private AudioSource buttonPressed;

    private float timeSinceLastClick;
    private float timeSinceClicks = 1.0f;
    private void Awake()
    {
        buttonPressed= GetComponent<AudioSource>();
    }
    private void Update()
    {
        timeSinceLastClick += Time.deltaTime;
        if (timeSinceLastClick < timeSinceClicks)
        {
            return;
        }
        if (areTheCredits)
        {
            timeSinceLastClick = 0;
            if (Input.GetAxisRaw("Cancel") != 0)
            {
                PlayButtonPressed();
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
        PlayButtonPressed();
        StartCoroutine(levelLoader.LoadScene(2));
    } 
    public void ChangeSceneToOptions()
    {
        PlayButtonPressed();
        StartCoroutine(levelLoader.LoadScene(4));
    }
    public void ExitGame()
    {
        PlayButtonPressed();
        Application.Quit();
    }
    public void ChangeSceneToMainMenu()
    {
        Time.timeScale= 1;
        PlayButtonPressed();
        StartCoroutine(levelLoader.LoadScene(1));
    }
    public void ChangeSceneToCharacterController()
    {
        PlayButtonPressed();
        StartCoroutine(levelLoader.LoadScene(3));
    }
    public void PlayButtonPressed()
    {
        buttonPressed.Play();
    }
}
