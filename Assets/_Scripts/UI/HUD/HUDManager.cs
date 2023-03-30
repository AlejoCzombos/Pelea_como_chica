using ControlPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioMixer audioMixer;

    private float oldVolume;

    private bool pauseMenuIsActive = false;
    public bool canPause;

    private void Awake()
    {
        audioMixer.GetFloat("Master", out oldVolume);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Pause") && canPause)
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuIsActive = !pauseMenuIsActive;
        pauseMenu.SetActive(pauseMenuIsActive);
        Time.timeScale = !pauseMenuIsActive ? 1.0f : 0.0f;
    }

    public void MuteVolume(bool isMute)
    {
        audioMixer.SetFloat("Master", (isMute ? 0f : oldVolume));
    }
}
