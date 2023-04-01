using ControlPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> imagenes;

    private float oldVolume;

    private bool pauseMenuIsActive = false;
    public bool canPause;

    private void Awake()
    {
        audioMixer.GetFloat("Master", out oldVolume);
    }
    private void Update()
    {
        heart();

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
    public void ActiveDeathScreen()
    {
        ResumeGame();
        deathScreen.SetActive(true);
    }
    public void ReplayGame()
    {
        deathScreen.SetActive(false);
        ResumeGame();
        //Cargar Checkpoint
    }

    public void MuteVolume(bool isMute)
    {
        audioMixer.SetFloat("Master", (isMute ? 0f : oldVolume));
    }

    void heart()
    {
        if (player.GetComponent<PlayerAttack>().currentHealth == 2)
        {
            animator.SetInteger("Hitted", 1);
            imagenes[2].gameObject.SetActive(false);

        }
        else if (player.GetComponent<PlayerAttack>().currentHealth == 1)
        {
            animator.SetInteger("Hitted", 2);
            imagenes[1].gameObject.SetActive(false);
        }
        else if (player.GetComponent<PlayerAttack>().currentHealth == 0)
        {
            animator.SetInteger("Hitted", 3);
            imagenes[0].gameObject.SetActive(false);
        }
        else if (animator.GetInteger("Hitted") == 0)
        {
            imagenes[0].gameObject.SetActive(true);
            imagenes[1].gameObject.SetActive(true);
            imagenes[2].gameObject.SetActive(true);

        }

    }

}
