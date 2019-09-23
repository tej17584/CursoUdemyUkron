using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;

    public float moveSpeed;

    //Para mover al personaje por el mundo
    private Vector3 moveDirection;

    //Variable del character controller
    public CharacterController charController;

    //Para la gravedad
    public float gravityScale = 5f;

    //Camara
    private Camera theCam;


    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la camara
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = moveDirection.y;
        //Parte de la dirección del movimiento
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveDirection *= moveSpeed;
        moveDirection.y = yStore;
        //Hacemos el if por si salta 
        if (Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

        //Transformamos la posicion del jugador
        //transform.position += (moveDirection * Time.deltaTime * moveSpeed);
        charController.Move(moveDirection * Time.deltaTime);
        //Rotacion para la camara
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
        }
    }
}