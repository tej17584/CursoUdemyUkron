﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public GameObject cpOn, cpOff;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))    
        {
            GameMaganer.instance.SetSpawnPoint(transform.position);
            //Array de chekcpoints
            CheckPoints[] allCP = FindObjectsOfType<CheckPoints>();
            for (int i = 0; i < allCP.Length; i++)
            {
                allCP[i].cpOff.SetActive(true);
                allCP[i].cpOn.SetActive(false);
            }
            //Activamos el on
            cpOff.SetActive(false);
            cpOn.SetActive(true);
        }
    }
}