using ControlPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private bool pauseMenuIsActive = false;
    public bool canPause;
    private void Update()
    {
        if (Input.GetButtonDown("Pause") && canPause)
        {
            pauseMenuIsActive = !pauseMenuIsActive;
            pauseMenu.SetActive(pauseMenuIsActive);
            Time.timeScale = !pauseMenuIsActive ? 1.0f : 0.0f;
        }
    }

    public void ChangeSceneToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ResumeGame()
    {
        pauseMenuIsActive = !pauseMenuIsActive;
        pauseMenu.SetActive(pauseMenuIsActive);
        Time.timeScale = !pauseMenuIsActive ? 1.0f : 0.0f;
    }

}