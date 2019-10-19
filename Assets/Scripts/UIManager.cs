using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Slider para musica

    public Slider musicVolSlider, sfxVolSlider;

    //game object para la pause
    public GameObject pauseScreen, optionsScreen;

    public Image blackScreen;

    //velocidad
    public float fadeSpeed = 2f;

    //Transición
    public bool fadeToBlack, fadeFromBlack;

    //Texto para el canvas
    public Text healthText;

    //Imagen de la barra
    public Image healthImage;
    // Start is called before the first frame update

    //public text
    public Text coinText;

    //Variables para escenas
    public string levelSelect, mainMenuScene;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack)
        {
            //hacemos un degradado
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b,
                Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            //Cambiamos el color
            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            //hacemos un degradado
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b,
                Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            //Cambiamos el color
            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    public void Resume()
    {
        GameMaganer.instance.PauseUnPause();
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1f;
    }

    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    public void SetSfxLevel()
    {
        AudioManager.instance.SetFxLevel();
    }
}