using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    //Referencia a todos los valores de música
    public AudioSource[] music;
    //array para los efectos de sonido

    public AudioSource[] soundEffects;

    //Nivel de la música
    public int levelMusictoPlay;

    //Variable para el mixer
    public AudioMixerGroup musicMixer, sfxMixer;

    //Canción actual
    //private int currenTrack;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //cuanido iniciemos el nivel toca la primera
        PlayMusic(levelMusictoPlay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlaySfx(5);
        }
    }

    public void PlayMusic(int musicToPlay)
    {
        for (int i = 0; i < music.Length; i++)
        {
            //En esta posición en particular que se detenga
            music[i].Stop();
        }

        //Ponemos la música que queremos
        music[musicToPlay].Play();
    }

    public void PlaySfx(int sfxToPlay)
    {
        soundEffects[sfxToPlay].Play();
    }

    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("MusicVolume", UIManager.instance.musicVolSlider.value);
    }

    public void SetFxLevel()
    {
        sfxMixer.audioMixer.SetFloat("SfxVol", UIManager.instance.sfxVolSlider.value);
    }
}