using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image blackScreen;

    //velocidad
    public float fadeSpeed = 2f;

    //Transición
    public bool fadeToBlack, fadeFromBlack;
    // Start is called before the first frame update

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
}