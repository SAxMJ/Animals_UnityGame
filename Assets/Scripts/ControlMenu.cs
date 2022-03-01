using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    // Start is called before the first frame
    public GameObject fondo1;
    public GameObject fondo2;
    public GameObject fondo3;

    public GameObject ventanaNJugadores;
    public GameObject ventanaSeleccionPersonaje;
    public GameObject botonInstrucciones;
    public GameObject menuPrinc;
    public GameObject cartelInstrucciones;
    public GameObject canvas;
    public GameObject cartelInfo;
    public GameObject cartelSalirConfirmacion;

    public Button pollo;
    public Button cerdo;
    public Button gato;
    public Button conejo;
    public Button raton;
    public Button zorro;
    public TMP_Text nJugadoresText;
    public GameObject salir;
    public GameObject info;

    public GameObject click;
    public GameObject musicaMenu;
    
    float velocidad = 1;
    
    bool multiJugador = false;
    int turnoSeleccionPersonaje = 0;
    int [,] MjugadorPersonaje=new int[2,6];
    int nJugadores = 0;
    int pers1=0;
    int pers2=0;
    int pers3=0;
    int pers4=0;
    int pers5=0;
    int pers6=0;

    string numJugadoresString = "numJugadoresString";
    string persJug1 = "persJug1";
    string persJug2 = "persJug2";
    string persJug3 = "persJug3";
    string persJug4 = "persJug4";
    string persJug5 = "persJug5";
    string persJug6 = "persJug6";

    void Start()
    {
        Instantiate(musicaMenu);
        menuPrinc.SetActive(true);
        ventanaNJugadores.SetActive(false);
        ventanaSeleccionPersonaje.SetActive(false);
        cartelInstrucciones.SetActive(false);
        cartelInfo.SetActive(false);
        cartelSalirConfirmacion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CompruebaPosicionFondo();
    }

    private void CompruebaPosicionFondo()
    {
        fondo1.transform.position -= new Vector3(velocidad * Time.deltaTime, 0);
        fondo2.transform.position -= new Vector3(velocidad * Time.deltaTime, 0);
        fondo3.transform.position -= new Vector3(velocidad * Time.deltaTime, 0);

        if (fondo1.transform.position.x <= -25)
        {
            fondo1.transform.position=new Vector3(19,-2.75f,0);
        }
        if (fondo2.transform.position.x <= -25)
        {
            fondo2.transform.position = new Vector3(19, -2.75f,0);
        }
        if (fondo3.transform.position.x <= -25)
        {
            fondo3.transform.position = new Vector2(19, -2.75f);
        }
    }

    public void UnJugador()
    {
        Instantiate(click);
        menuPrinc.SetActive(false);
        ventanaSeleccionPersonaje.SetActive(true);
        nJugadores = 1;
        salir.SetActive(false);
        info.SetActive(false);
    }

    public void VariosJugadores()
    {
        Instantiate(click);
        menuPrinc.SetActive(false);
        ventanaNJugadores.SetActive(true);
        nJugadores = 2;
        multiJugador = true;
        salir.SetActive(false);
        info.SetActive(false);
    }

    public void Instrucciones()
    {
        Instantiate(click);
        menuPrinc.SetActive(false);
        cartelInstrucciones.SetActive(true);
        salir.SetActive(false);
        info.SetActive(false);
    }

    public void SumaJugadores()
    {
        Instantiate(click);
        if (nJugadores < 6)
        {
            nJugadores++;
            nJugadoresText.text = nJugadores.ToString();
        }
    }

    public void RestaJugadores()
    {
        Instantiate(click);
        if (nJugadores > 2)
        {
            nJugadores--;
            nJugadoresText.text = nJugadores.ToString();
        }
    }

    public void SiguienteNumJugadores()
    {
        Instantiate(click);
        ventanaSeleccionPersonaje.SetActive(true);
        ventanaNJugadores.SetActive(false);
    }

    public void VolverDesdeSeleccPersonaje()
    {
        Instantiate(click);
        if (nJugadores > 1)
        {
            ventanaSeleccionPersonaje.SetActive(false);
            ventanaNJugadores.SetActive(true);
            ActivaTodosLosPersonajes();
        }
        else
        {
            menuPrinc.SetActive(true);
            ventanaSeleccionPersonaje.SetActive(false);
            salir.SetActive(true);
            info.SetActive(true);
        }
    }

    public void VolverDesdeNumJugadores()
    {
        Instantiate(click);
        menuPrinc.SetActive(true);
        ventanaNJugadores.SetActive(false);
        salir.SetActive(true);
        info.SetActive(true);
    }

    public void VolverDesdeInstrucciones()
    {
        Instantiate(click);
        cartelInstrucciones.SetActive(false);
        menuPrinc.SetActive(true);
        salir.SetActive(true);
        info.SetActive(true);
    }

    public void VolverDesdeInfo()
    {
        Instantiate(click);
        cartelInfo.SetActive(false);
        menuPrinc.SetActive(true);
        salir.SetActive(true);
        info.SetActive(true);
    }

    public void SiguienteSeleccionPersonaje()
    {
        Instantiate(click);
        //Se comprueba si se ha pulsado al menos un botón previamente a darle a siguiente
        if (MjugadorPersonaje[1,turnoSeleccionPersonaje]!=0)
            turnoSeleccionPersonaje++;

        if (multiJugador && turnoSeleccionPersonaje<nJugadores)
        {
            DesactivarPersonajeElegido();
            Debug.Log(turnoSeleccionPersonaje);

        }else if(multiJugador && turnoSeleccionPersonaje >= nJugadores)
        {
            //Empezamos la partida para varios jugadores
            GuardaInfoJugadores();
        }
        else
        {
            //Empezamos la partida para un jugador
            GuardaInfoJugadores();
        }
    }

    //Utilizaremos el turno de seleccion como posición de la matriz de personajes
    public void SeleccionaPollo() //1
    {
        MjugadorPersonaje[0, turnoSeleccionPersonaje] = turnoSeleccionPersonaje+1; //En la primera fila en la columna de su turno asignamos el numero de jugador
        MjugadorPersonaje[1, turnoSeleccionPersonaje] = 1;  //En la segunda fila, en la columna de su turno asignamos el numero de animal
    }

    public void SeleccionaZorro() //2
    {
        MjugadorPersonaje[0, turnoSeleccionPersonaje] = turnoSeleccionPersonaje + 1; 
        MjugadorPersonaje[1, turnoSeleccionPersonaje] = 2;
    }

    public void SeleccionaCerdo() //3
    {
        MjugadorPersonaje[0, turnoSeleccionPersonaje] = turnoSeleccionPersonaje + 1; 
        MjugadorPersonaje[1, turnoSeleccionPersonaje] = 3;
    }

    public void SeleccionaConejo() //4
    {
        MjugadorPersonaje[0, turnoSeleccionPersonaje] = turnoSeleccionPersonaje + 1; 
        MjugadorPersonaje[1, turnoSeleccionPersonaje] = 4;
    }

    public void SeleccionaRaton() //5
    {
        MjugadorPersonaje[0, turnoSeleccionPersonaje] = turnoSeleccionPersonaje + 1;
        MjugadorPersonaje[1, turnoSeleccionPersonaje] = 5;
    }

    public void SeleccionaGato() //6
    {
        MjugadorPersonaje[0, turnoSeleccionPersonaje] = turnoSeleccionPersonaje + 1;
        MjugadorPersonaje[1, turnoSeleccionPersonaje] = 6;
    }

    
    public void DesactivarPersonajeElegido()
    {
        switch(MjugadorPersonaje[1, turnoSeleccionPersonaje-1])
        {
            case 1:
                pollo.interactable = false;
                break;
            case 2:
                zorro.interactable=false;
                break;
            case 3:
                cerdo.interactable = false;
                break;
            case 4:
                conejo.interactable = false;
                break;
            case 5:
                raton.interactable = false;
                break;
            case 6:
                gato.interactable = false;
                break;
        }
    }

    public void ActivaTodosLosPersonajes()
    {
        pollo.interactable = true;

        zorro.interactable = true;
        
        cerdo.interactable = true;
        
        conejo.interactable = true;
        
        raton.interactable = true;
        
        gato.interactable = true;
    }

    private void GuardaInfoJugadores()
    {
        PlayerPrefs.SetInt(persJug1, MjugadorPersonaje[1,0]);
        
        PlayerPrefs.SetInt(persJug2, MjugadorPersonaje[1, 1]);
        
        PlayerPrefs.SetInt(persJug3, MjugadorPersonaje[1, 2]);
        
        PlayerPrefs.SetInt(persJug4, MjugadorPersonaje[1, 3]);
        
        PlayerPrefs.SetInt(persJug5, MjugadorPersonaje[1, 4]);
        
        PlayerPrefs.SetInt(persJug6, MjugadorPersonaje[1, 5]);
        
        PlayerPrefs.SetInt(numJugadoresString, nJugadores);

        SceneManager.LoadScene("SampleScene");
    }

    public void MostrarInfo()
    {
        Instantiate(click);
        cartelInfo.SetActive(true);
        menuPrinc.SetActive(false);
        salir.SetActive(false);
        info.SetActive(false);
    }

    public void Salir()
    {
        Instantiate(click);
        cartelSalirConfirmacion.SetActive(true);
        menuPrinc.SetActive(false);
        salir.SetActive(false);
        info.SetActive(false);
    }

    public void CartelSalirSalir()
    {
        Instantiate(click);
        Application.Quit();
    }

    public void CartelSalirVolver()
    {
        Instantiate(click);
        cartelSalirConfirmacion.SetActive(false);
        menuPrinc.SetActive(true);
        salir.SetActive(true);
        info.SetActive(true);
    }

}
