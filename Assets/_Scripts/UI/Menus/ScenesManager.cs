using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using UnityEngine.EventSystems;
using System.Linq;
using Unity.VisualScripting;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private bool areTheCredits;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private Animator musicAnimator;
    [SerializeField] private Animator MenuMusicAnimator;

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
        var objeto = GameObject.Find("Main Menu Music");
        MenuMusicAnimator = objeto.GetComponent<Animator>();
        Time.timeScale = 1;
        PlayButtonPressed();
        MenuMusicAnimator.SetTrigger("FadeOut");
        StartCoroutine(levelLoader.LoadScene(1, 1.3f));
    }
    public void ChangeSceneToCredits()
    {
        PlayButtonPressed();
        StartCoroutine(levelLoader.LoadSceneMenu(2));
    } 
    public void ChangeSceneToOptions()
    {
        PlayButtonPressed();
        StartCoroutine(levelLoader.LoadSceneMenu(4));
    }
    public void ExitGame()
    {
        PlayButtonPressed();
        Application.Quit();
    }
    public void ChangeSceneToMainMenu()
    {
        PlayButtonPressed();
        StartCoroutine(levelLoader.LoadSceneMenu(0));
    }
    public void ChangeSceneToMainMenuInGame()
    {
        Time.timeScale = 1;
        PlayButtonPressed();
        musicAnimator.SetTrigger("FadeOut");
        StartCoroutine(levelLoader.LoadScene(0,3));
    }
    public void ChangeSceneToCharacterController()
    {
        PlayButtonPressed();
        StartCoroutine(levelLoader.LoadSceneMenu(3));
    }
    public void ChangeSceneToFinal()
    {
        PlayButtonPressed();
        StartCoroutine(levelLoader.LoadSceneMenu(5));
    }
    public void PlayButtonPressed()
    {
        buttonPressed.Play();
    }
    public void Restart() {
        StartCoroutine(levelLoader.LoadScene(1, 0));
        Time.timeScale = 1;
    }

}
