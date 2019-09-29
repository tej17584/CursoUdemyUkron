using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaganer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        //Para bloquear el cursos
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
    }
}