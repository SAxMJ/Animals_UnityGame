using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    Animator anim;
    public FixedJoystick joystickV;
    public FixedJoystick joystickH;
    public float destino;
    public bool movimientoD,movimientoI,movimiento,movimientoAr,movimientoAb;
    public bool primerMovAbajo=true;
    public bool primerMovimiento=true;
    public bool vivo = true;
    public bool temporizador = true;
    bool sueloTocado = false;
    public GameObject sangre;
    public GameObject animalMuerto;

    public GameObject acuchilla1;
    public GameObject acuchilla2;
    public GameObject acuchilla3;
    public GameObject acuchilla4;
    public GameObject acuchilla5;

    // Start is called before the first frame update
    void Start()
    {
        joystickV = GameObject.FindGameObjectWithTag("vertical").GetComponent<FixedJoystick>();
        joystickH = GameObject.FindGameObjectWithTag("horizontal").GetComponent<FixedJoystick>();
        anim=this.GetComponent<Animator>();
        movimientoD = false;
        movimientoI = false;
        movimientoAr = false;
        movimientoAb = false;
        movimiento = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (vivo && !temporizador)
        {

            if ((Input.GetKeyDown("w") | joystickV.Vertical > 0) && movimiento == false && transform.position.y < 4.5)
            {
                anim.SetBool("Arr", true);
                anim.SetBool("Aba", false);
                anim.SetBool("Der", false);
                anim.SetBool("Izq", false);

                movimiento = true;
                movimientoAr = true;

                destino = this.transform.position.y + 1;

                if (destino == 5)
                {
                    destino = 4.5f;
                }
                primerMovimiento = false;
            }
            if ((Input.GetKeyDown("a") || joystickH.Horizontal < 0) && movimiento == false && transform.position.x > -6)
            {
                anim.SetBool("Arr", false);
                anim.SetBool("Aba", false);
                anim.SetBool("Der", false);
                anim.SetBool("Izq", true);

                movimiento = true;
                movimientoI = true;

                destino = this.transform.position.x - 1;
                primerMovimiento = false;

            }
            if ((Input.GetKeyDown("s") || joystickV.Vertical < 0) && movimiento == false && transform.position.y > -4)
            {
                anim.SetBool("Arr", false);
                anim.SetBool("Aba", true);
                anim.SetBool("Der", false);
                anim.SetBool("Izq", false);

                movimiento = true;
                movimientoAb = true;

                destino = this.transform.position.y - 1;

                if (primerMovAbajo)
                {
                    destino = this.transform.position.y - 0.5f;
                }
                primerMovimiento = false;

            }
            if ((Input.GetKeyDown("d") || joystickH.Horizontal > 0) && movimiento == false && transform.position.x < 6)
            {
                anim.SetBool("Arr", false);
                anim.SetBool("Aba", false);
                anim.SetBool("Der", true);
                anim.SetBool("Izq", false);

                movimiento = true;
                movimientoD = true;

                destino = this.transform.position.x + 1;
                primerMovimiento = false;
            }

            if (movimiento)
            {
                muevePersonaje();
            }
        }
    }


    private void muevePersonaje()
    {
        if (movimientoAr)
        {
            this.transform.position += new Vector3(0, 0.01f, 0);
            if (this.transform.position.y > destino)
            {
                if (destino == 4.5)
                {
                    this.transform.position = new Vector3(this.transform.position.x, destino, 0);
                    primerMovAbajo = true;
                }
                else
                {
                    this.transform.position = new Vector3(this.transform.position.x, (int)destino, 0);
                }
                movimiento = false;
                movimientoAr = false;
            }
        }

        if (movimientoAb)
        {
            this.transform.position += new Vector3(0, -0.01f, 0);
            if (this.transform.position.y < destino)
            {
                this.transform.position = new Vector3(this.transform.position.x, (int)destino, 0);
                movimiento = false;
                movimientoAb = false;
                primerMovAbajo = false;
            }
        }

        if (movimientoD)
        {
            this.transform.position += new Vector3(0.01f, 0, 0);
            if (this.transform.position.x > destino)
            {
                this.transform.position = new Vector3((int)destino, this.transform.position.y, 0);
                movimiento = false;
                movimientoD = false;
            }
        }

        if (movimientoI)
        {
            
            this.transform.position += new Vector3(-0.01f, 0, 0);
            if (this.transform.position.x <= destino)
            {
                this.transform.position = new Vector3((int)destino, this.transform.position.y, 0);
                movimiento = false;
                movimientoI = false;

            
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Suelo" && !sueloTocado)
        {
            sueloTocado = true;
            Instantiate(animalMuerto, new Vector2(this.transform.position.x, this.transform.position.y), animalMuerto.transform.rotation);
            this.gameObject.GetComponent<SpriteRenderer>().enabled=false;
            Instantiate(sangre, new Vector2(this.transform.position.x, this.transform.position.y),Quaternion.identity);
            ReproduceSonidoPinchos();
        }
    }

    private void ReproduceSonidoPinchos()
    {
        System.Random rand = new System.Random();

        switch (rand.Next(1, 6))
        {
            case 1: Instantiate(acuchilla1);
                    break;
            case 2:
                Instantiate(acuchilla2);
                break;
            case 3:
                Instantiate(acuchilla3);
                break;
            case 4:
                Instantiate(acuchilla4);
                break;
            case 5:
                Instantiate(acuchilla5);
                break;
        }
    }
}
