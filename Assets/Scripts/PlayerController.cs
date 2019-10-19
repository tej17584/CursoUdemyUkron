using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Instancia dle player controller
    public static PlayerController instance;
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

    //Variables para que se haga hacia atrás al golpear unas espinas
    public bool isKnocking;
    public float knockBackLength = .5f;
    private float _knockBackCounter;
    public Vector2 knockBackPower;

    //Variables para animacion de dolor de espinas
    public GameObject[] playerPieces;

    //Variable para la fuerza al matar al enemigo
    public float bounceForce = 8f;

    public bool stopMove;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la camara
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnocking && !stopMove)
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
        }

        if (isKnocking)
        {
            _knockBackCounter -= Time.deltaTime;
            //Se guarda el movimiento
            float yStore = moveDirection.y;
            //Para que el jugador vea
            moveDirection = playerModel.transform.forward * -knockBackPower.x;
            //Parte de la dirección del movimiento
            moveDirection.y = yStore;
            if (charController.isGrounded)
            {
                //Si estamos en el piso, que no cambie la gravedad
                moveDirection.y = 0f;
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            //Lo movemos con la nueva fuerza
            charController.Move(moveDirection * Time.deltaTime);

            if (_knockBackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        if (stopMove)
        {
            moveDirection = Vector3.zero;
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            charController.Move(moveDirection);
        }

        Anim.SetFloat("Speed", Math.Abs(moveDirection.x) + Math.Abs(moveDirection.z));
        Anim.SetBool("Grounded", charController.isGrounded);
    }

    //Este método es para lanzar al player hacia atrás.
    public void knockBack()
    {
        isKnocking = true;
        _knockBackCounter = knockBackLength;
        Debug.Log("Knocked back");
        moveDirection.y = knockBackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }

    public void Bounce()
    {
        moveDirection.y = bounceForce;
        charController.Move(moveDirection * Time.deltaTime);
    }
}