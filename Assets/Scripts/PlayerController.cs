using System;
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

    //Playermodel
    public GameObject playerModel;

    //Velocidad de rotacion
    public float rotateSpeed;

    //Animacion
    public Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la camara
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Se guarda el movimiento
        float yStore = moveDirection.y;
        //Para que el jugador vea
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) +
                        (transform.right * Input.GetAxisRaw("Horizontal"));
        //Parte de la dirección del movimiento
        //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        //Normalizamos
        moveDirection.Normalize();
        moveDirection *= moveSpeed;
        moveDirection.y = yStore;
        //Si esta en el suelo
        if (charController.isGrounded)
        {    
            //Si estamos en el piso, que no cambie la gravedad
            moveDirection.y = 0f;
            //Hacemos el if por si salta 
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

        //Transformamos la posicion del jugador
        //transform.position += (moveDirection * Time.deltaTime * moveSpeed);
        charController.Move(moveDirection * Time.deltaTime);
        //Rotacion para la camara
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            //playerModel.transform.rotation = newRotation;
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation,
                rotateSpeed * Time.deltaTime);
        }

        Anim.SetFloat("Speed", Math.Abs(moveDirection.x) + Math.Abs(moveDirection.z));
        Anim.SetBool("Grounded", charController.isGrounded);
    }
}