using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;


    //Variables para la saluda
    public int currenHealth, maxHealth;

    //Variables para invinsibilidad y salud
    public float invincibleLength = 2f;
    private float invictCounter;

    
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currenHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invictCounter > 0)
        {
            invictCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController.instance.playerPieces.Length; i++)
            {
                if (Mathf.Floor(invictCounter * 5f) % 2 == 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.playerPieces[i].SetActive(false);
                }

                if (invictCounter <= 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
            }
        }
    }

    //M+étodo para Hurt
    public void Hurt()
    {
        if (invictCounter <= 0)
        {
            currenHealth -= 1;
            if (currenHealth <= 0)
            {
                //Si nuestra saluds es abajo de 0 o 0, la colocamos a 0 y respawn() al player
                currenHealth = 0;
                GameMaganer.instance.Respawn();
            }
            else
            {
                PlayerController.instance.knockBack();
                invictCounter = invincibleLength;
            }
        }
    }

    public void ResetHealth()
    {
        currenHealth = maxHealth;
    }

    public void AddHealth(int AmountToHeal)
    {
        currenHealth += AmountToHeal;
        if (currenHealth > maxHealth)
        {
            currenHealth = maxHealth;
        }
    }
}