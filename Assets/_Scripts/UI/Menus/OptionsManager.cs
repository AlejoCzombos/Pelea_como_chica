using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider EnviromentSlider;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource audioClip;

    private void Awake()
    {
        float music;
        float sfx;
        float envir;
        audioMixer.GetFloat("Music", out music);
        audioMixer.GetFloat("SFX", out sfx);
        audioMixer.GetFloat("Enviroment", out envir);

        fullScreenToggle.isOn = (Screen.fullScreen == true ? true : false);

        MusicSlider.value = music;
        SFXSlider.value = sfx;
        EnviromentSlider.value = envir;

        SFXSlider.onValueChanged.AddListener(delegate { AjustSFXVolume(); });
        MusicSlider.onValueChanged.AddListener(delegate {   AjustMusicVolume(); });
        EnviromentSlider.onValueChanged.AddListener(delegate { AjustEnviromentVolume(); });
    }

    public void SetFullscreen(bool isFullscreen)
    {
        audioClip.Play();
        Screen.fullScreen = isFullscreen;
    }
    public void AjustMusicVolume()
    {
        audioMixer.SetFloat("Music", MusicSlider.value);
    }
    public void AjustSFXVolume()
    {
        audioMixer.SetFloat("SFX", SFXSlider.value);
    }
    public void AjustEnviromentVolume()
    {
        audioMixer.SetFloat("Enviroment", EnviromentSlider.value);
    }
}
