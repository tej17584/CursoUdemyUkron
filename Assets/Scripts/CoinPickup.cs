using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    //Valor de las monedas
    public int value;

    //efecto de la moneda
    public int soundtoPlay;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Sabemos que tenemos que hacer un trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameMaganer.instance.AddCoins(value);
            Destroy(gameObject);
            AudioManager.instance.PlaySfx(soundtoPlay);
        }
    }
}