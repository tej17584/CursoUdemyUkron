﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaganer : MonoBehaviour
{
    //Creamos una instancia para no arrastrar
    public static GameMaganer instance;

    //Vector para respawn 
    private Vector3 respawnPosition;

    //Efecto para morir
    public GameObject deathEffect;


    //Monedas
    public int currentCoins;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        //Para bloquear el cursos
        Cursor.lockState = CursorLockMode.Locked;
        //Tomamos la posicion en la que el playaer está 
        respawnPosition = PlayerController.instance.transform.position;

        //Agregamos monedas vacías
        AddCoins(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }

    //Agregamos una funcion
    public void Respawn()
    {
        //iniciamos la corutina
        StartCoroutine(RespawnCo());
        HealthManager.instance.PlayerKilled();
    }

    public IEnumerator RespawnCo()
    {
        //Lo desaparecemos de la escena
        PlayerController.instance.gameObject.SetActive(false);
        //Quitamos la camara
        CameraController.instance.theCMBrain.enabled = false;
        //iniciamos el fade del background
        UIManager.instance.fadeToBlack = true;
        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f),
            PlayerController.instance.transform.rotation);

        //Ponemos el tiempo de espera
        yield return new WaitForSeconds(2f);
        //Le devolvemos la vida
        HealthManager.instance.ResetHealth();
        //Ponemos al reves el fade del background
        UIManager.instance.fadeFromBlack = true;
        //Llamamos al respawn
        PlayerController.instance.transform.position = respawnPosition;
        //Lo volvmemos a poner
        CameraController.instance.theCMBrain.enabled = true;
        //Lo volvemos a hacer aparecer
        PlayerController.instance.gameObject.SetActive(true);
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("Spawn point set");
    }

    //metodo de monedas
    public void AddCoins(int coinsAdd)
    {
        currentCoins += coinsAdd;
        UIManager.instance.coinText.text = "" + currentCoins.ToString();
    }

    //método para la pausa
    public void PauseUnPause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
            //Para bloquear el cursor
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;
            Cursor.visible = true;
            //Para bloquear el cursos
            Cursor.lockState = CursorLockMode.None;
        }
    }
}