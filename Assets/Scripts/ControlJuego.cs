using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJuego : MonoBehaviour
{
    public GameObject pincho;          //valor 1
    public GameObject bomba;          //valor 2
    public GameObject zanahoria;     //valor 10
    public GameObject remolacha;    //valor 11
    public GameObject cebolla;     //valor 12
    public GameObject tomate;     //valor 13
    public GameObject rabano;    //valor 14
    public GameObject calabacin;//valor 15
    public GameObject pinchoAb;
    public GameObject bandera; //valor 8
    public GameObject agujero;
    public GameObject polloExplosion;
    public GameObject suelo;
    public GameObject cartelMuerto;
    public GameObject cartelBandera;
    public GameObject cartelFinalOnePlay;
    public GameObject menuPausa;

    public TMP_Text textoRonda;
    public TMP_Text textoPuntosRonda;
    public TMP_Text textoPuntosTotales;
    public TMP_Text textoPuntosFinales;

    //PERSONAJES
    public GameObject gato;
    public GameObject pollo;
    public GameObject cerdo;
    public GameObject conejo;
    public GameObject raton;
    public GameObject zorro;
    public GameObject pando;

    public GameObject jugador;

    public int[,] matEnterrados=new int[9,13]; //117 celdas útiles
    public int ronda=1;
    public int turno = 1;
    int posXbandera , posYbandera = 8;
    int porcentajeTrampas; //15% 20% 25% 30% 35%
    int porcentajePuntos; //12% 11% 10% 9% 8%
    public int puntos = 0;
    public int puntosTot = 0;

    bool exito = false;
    bool vivo = true;
    bool primerMovimiento = true;
    public bool cuentaAtrasTerminada=false;
    bool banderaAlcanzada = false;
    public bool pichoCreado = false;

    float posFilaPersonaje, posColumnaPersonaje;
    public float cuentaAtras = 5;
    public float cuentaAtrasDespuesMorir = 4;

    //INFORMACIÓN QUE VIENE DESDE EL MENU
    int nJugadores = 0;
    int pers1;
    int pers2;
    int pers3;
    int pers4;
    int pers5;
    int pers6;

    string numJugadoresString = "numJugadoresString";
    string persJug1 = "persJug1";
    string persJug2 = "persJug2";
    string persJug3 = "persJug3";
    string persJug4 = "persJug4";
    string persJug5 = "persJug5";
    string persJug6 = "persJug6";

    public GameObject sonidoClick;
    public GameObject sonidoExplosion1;
    public GameObject sonidoExplosion2;
    public GameObject sonidoVerdura;
    public GameObject musicaFondo1;
    public GameObject musicaFondo2;
    public GameObject musicaFondo3;
    public GameObject musicaFondo4;
    public GameObject musicaFondo5;

    //posXPersonaje=posCEnterrados-6
    //posYPersonaje=3-posFEnterradoos+0.5

    void Start()
    {
        this.CargarInfoJugadores();
        this.InstanciaBandera();
        this.InstanciaJugador();
        this.generarPosicionTrampas();
        this.GenerarPosicionPuntos();
        this.instanciaObjetosEnPantalla();
        this.InstanciaMusicaDeFondo();
        cartelMuerto.SetActive(false);
        cartelBandera.SetActive(false);
        cartelFinalOnePlay.SetActive(false);
        menuPausa.SetActive(false);
        ronda = 1;
        //jugador = GameObject.FindWithTag("Player");
    }


    void Update()
    {
        if (vivo && cuentaAtras<=0 && cuentaAtrasTerminada)
        {
            if (primerMovimiento)
            {
                primerMovimiento = jugador.GetComponent<Jugador>().primerMovimiento;
            }

            if (!jugador.GetComponent<Jugador>().movimiento && !(jugador.GetComponent<Transform>().transform.position.y == 4.5f)
                && primerMovimiento == false)
            {

                posFilaPersonaje = -(jugador.GetComponent<Transform>().transform.position.y) + 4 + 0.5f;
                posColumnaPersonaje = (jugador.GetComponent<Transform>().transform.position.x) + 6;

                Debug.Log("PosicionPersonaje: " + (int)posFilaPersonaje + " " + (int)posColumnaPersonaje);
                Debug.Log("ContenidoMatrizEnterrados: " + matEnterrados[(int)posFilaPersonaje, (int)posColumnaPersonaje]);

                if (posFilaPersonaje >= 0)
                {
                    if (matEnterrados[(int)posFilaPersonaje, (int)posColumnaPersonaje] == 1 ||
                        matEnterrados[(int)posFilaPersonaje, (int)posColumnaPersonaje] == 2)
                    {
                        vivo = false;
                        jugador.GetComponent<Jugador>().vivo = false;

                        Debug.Log("Muerto");
                      
                        if (matEnterrados[(int)posFilaPersonaje, (int)posColumnaPersonaje] == 1)
                        {
                            Instantiate(agujero, new Vector2(jugador.transform.position.x, jugador.transform.position.y - 0.5f), Quaternion.identity);
                            jugador.GetComponent<Explodable>().generateFragments();
                            jugador.GetComponent<ExplodeOnClick>().explotaJugador();
                            InstanciaSonidoExplosion();
                            Instantiate(suelo, new Vector2(0, jugador.transform.position.y - 0.3f), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(pinchoAb, new Vector2(jugador.transform.position.x, jugador.transform.position.y - 0.2f), Quaternion.identity);
                            pichoCreado = true;
                            
                        }

                        jugador.GetComponent<Jugador>().temporizador = true;
                        puntos = 0;
                        cuentaAtrasTerminada = false;
                    }

                    if (matEnterrados[(int)posFilaPersonaje, (int)posColumnaPersonaje] >= 10 &&
                       matEnterrados[(int)posFilaPersonaje, (int)posColumnaPersonaje] <= 15)
                    {
                        Debug.Log("Punto");
                        //Marcaremos la casilla a 0 para que no vuelva a entrar en la condicion y solo coja el punto una vez
                        Instantiate(sonidoVerdura);
                        Instantiate(agujero, new Vector2(jugador.transform.position.x, jugador.transform.position.y - 0.5f), Quaternion.identity);
                        matEnterrados[(int)posFilaPersonaje, (int)posColumnaPersonaje] = 0;
                        puntos++;
                    }

                    if (matEnterrados[(int)posFilaPersonaje, (int)posColumnaPersonaje] ==8 && !banderaAlcanzada)
                    {
                        Debug.Log("Llegaste a la bandera!!!!");
                        textoPuntosRonda.text = "+" + puntos;
                        puntos = puntos + 10;
                        banderaAlcanzada = true;

                        if (ronda < 5)
                        {
                            cartelBandera.SetActive(true);
                        }
                        else
                        {
                            cartelFinalOnePlay.SetActive(true);
                            puntosTot += puntos;
                            textoPuntosFinales.text = "Total points: " + puntosTot;
                        }
                    }
                }
            }
        }

        if (pichoCreado)
        {
            Debug.Log("estado " + (pinchoAb.GetComponent<Pinchos>().pinchosAbiertos));
           // if (pinA.GetComponent<Pinchos>().pinchosAbiertos==true)
            //{
                Instantiate(suelo, new Vector2(0, jugador.transform.position.y - 0.5f), Quaternion.identity);
                jugador.GetComponent<Rigidbody2D>().gravityScale = 1;
                jugador.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 8), ForceMode2D.Impulse);
                pichoCreado = false;
            //}
        }

        if(!cuentaAtrasTerminada && cuentaAtras<=0)
        {
            jugador = GameObject.FindWithTag("Player");
            cuentaAtrasTerminada = true;
            this.destruyeObjetos();
            jugador.GetComponent<Jugador>().temporizador = false;
        }

        if (!vivo)
        {
            if (cuentaAtrasDespuesMorir <= 0)
            {
                if (ronda < 5)
                {
                    cartelMuerto.SetActive(true);
                }
                else
                {
                    cartelFinalOnePlay.SetActive(true);
                    puntosTot += puntos;
                    textoPuntosFinales.text = "Total points: " + puntosTot;
                }
            }
            cuentaAtrasDespuesMorir -= Time.deltaTime;
        }

        if (Input.GetKey("escape"))
        {
            EscapePulsado();
        }
            cuentaAtras -= Time.deltaTime;
    }

    //Se mostrará la bandera por pantalla y además se calcularán sus coordenadas dentro de la matriz de juego
    private void InstanciaBandera()
    {
        //bandera = new GameObject();
        System.Random rand1 = new System.Random();
        System.Random rand = new System.Random(rand1.Next(0,100));

        float psx = rand.Next(-6, 7);
        Instantiate(bandera, new Vector2(psx, -4.2f), Quaternion.identity);

        posXbandera = (int)psx+6;
        posYbandera = 8;
        Debug.Log("posicionB" + posYbandera + " " + posXbandera);

        matEnterrados[posYbandera,posXbandera] = 8;

        Debug.Log("Mat[posY,posX]" + matEnterrados[posYbandera, posXbandera]);

    }

    private void InstanciaJugador()
    {
        System.Random rand = new System.Random();
        float psx = rand.Next(-6, 7);

        switch (pers1)
        {
            case 1:
                Instantiate(pollo, new Vector2(psx, 4.5f), Quaternion.identity);
                break;
            case 2:
                Instantiate(zorro, new Vector2(psx, 4.5f), Quaternion.identity);
                break;
            case 3:
                Instantiate(cerdo, new Vector2(psx, 4.5f), Quaternion.identity);
                break;
            case 4:
                Instantiate(conejo, new Vector2(psx, 4.5f), Quaternion.identity);
                break;
            case 5:
                Instantiate(raton, new Vector2(psx, 4.5f), Quaternion.identity);
                break;
            case 6:
                Instantiate(gato, new Vector2(psx, 4.5f), Quaternion.identity);
                break;

                //Si no se ha elegido nungún personaje, instanciaremos a pando como personaje predeterminado
            default:
                Instantiate(pando, new Vector2(psx, 4.5f), Quaternion.identity);
                break;
        }

    }

    private void generarPosicionTrampas()
    {
        int posF, posC;
        //matEnterrados[8, (int) bandera.transform.position.x+6] = 8; //la posición de la bandera se marcará con un 8

        while (!exito)
        {
            for (int i = 0; i < ((15 + (ronda * 5 - 5)) * 117) / 100; i++)
            {
                System.Random rand = new System.Random();
                do
                {
                    //Primero determinamos la posicion de la trampa
                    posF = rand.Next(0, 9); // Entre 0 y 9
                    posC = rand.Next(0, 13); //Entre 0 y 13
                } while (matEnterrados[posF, posC] != 0); ///ESTAS DOS CONDICIONES SOLO SON PARA PROBAR QUE NO SE GENEREN BOMBAS EN LOSPUNTOS INICIALES Y FINALES PREDETERMINADOS

                Debug.Log(posF + "" + posC);
                //Segundo determinamos que tipo de trampa es (1 pincho 2 bomba)
                matEnterrados[posF, posC] = rand.Next(1, 3);

            }
            //Comprobamos que la posición generada de trampas se posible de cruzar hasta la bandera
            backTrackingCaminoPosible(0, 0);

            //Si no hubo éxito restablecemos la matriz con todos los valores a 0
            if (!exito)
            {
                matEnterrados= new int[9, 13];
                matEnterrados[8, posXbandera] = 8; //la posición de la bandera se marcará con un 8
            }
        }
        Debug.Log("Exito");

    }

    private void GenerarPosicionPuntos()
    {
        int posF, posC;
        int it=0;
        for (int i = 0; i < ((12 + (ronda * 1 - 1)) * 117) / 100; i++)
        {
            System.Random rand = new System.Random();
            do
            {
                //Primero determinamos la posicion del punto
                posF = rand.Next(0, 9); // Entre 0 y 9
                posC = rand.Next(0, 13); //Entre 0 y 13
            } while (matEnterrados[posF, posC] !=0 && matEnterrados[posF,posC]!=3);
            it++;
            Debug.Log("it " + it);
            Debug.Log("PosicionPunto: "+posF + " " + posC);
            //Segundo determinamos que tipo de punto es
            matEnterrados[posF, posC] = rand.Next(10, 16);

        }
    }
    private void instanciaObjetosEnPantalla()
    {
        for(int i=0; i<9; i++)
        {
            for(int j=0; j<13; j++)
            {
                switch (matEnterrados[i, j])
                {
                    case 1:
                        Instantiate(bomba, new Vector2(j - 6, 3 - i + 0.5f), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(pincho, new Vector2(j - 6, 3 - i + 0.8f), Quaternion.identity);
                        break;
                    case 10:
                        Instantiate(zanahoria, new Vector2(j - 6, 3 - i + 0.5f), Quaternion.identity);
                        break;
                    case 11:
                        Instantiate(remolacha, new Vector2(j - 6, 3 - i + 0.5f), Quaternion.identity);
                        break;
                    case 12:
                        Instantiate(cebolla, new Vector2(j - 6, 3 - i + 0.5f), Quaternion.identity);
                        break;
                    case 13:
                        Instantiate(tomate, new Vector2(j - 6, 3 - i + 0.5f), Quaternion.identity);
                        break;
                    case 14:
                        Instantiate(rabano, new Vector2(j - 6, 3 - i + 0.5f), Quaternion.identity);
                        break;
                    case 15:
                        Instantiate(calabacin, new Vector2(j - 6, 3 - i + 0.5f), Quaternion.identity);
                        break;
                }
            }
        }
    }

    private void destruyeObjetos()
    {
        GameObject[] objetos;
        objetos = GameObject.FindGameObjectsWithTag("Trampa");

        foreach (GameObject objeto in objetos )
        {
            Destroy(objeto);
        }

        objetos = GameObject.FindGameObjectsWithTag("Puntos");

        foreach (GameObject objeto in objetos)
        {
            Destroy(objeto);
        }
    }
    //Utilizaremos un algoritmo de backtracking para comprobar si es posible atravesar el mapa sin pisar una trampa
    //Desde la fila 0 de la matriz a la última fila
    private void backTrackingCaminoPosible(int posX, int posY)
    {

        int[] movX = { 1, -1, 0, 0 };
        int[] movY = { 0, 0, 1, -1 };

        int posMov = 0;
        int u, v;


        while (exito == false && posMov < 4)
        {
            //Si estamos en la primera columna de la matriz, no trataremos de movernos -1 posiciones porque salimos de la matriz
            if(posX ==0 && movX[posMov] == -1)
            {
                posMov++;
                continue;
            }

            if (posX == 8 && movX[posMov] == 1)
            {
                posMov++;
                continue;
            }

            //Si estamos en la primera fila de la matriz, no trataremos de movernos -1 posiciones porque salimos de la matriz
            if (posY == 0 && movY[posMov] == -1)
            {
                posMov++;
                continue;
            }

            if (posY == 12 && movY[posMov] == 1)
            {
                posMov++;
                continue;
            }

            u = posX + movX[posMov];
            v = posY + movY[posMov];

            if (u >= 0 && u < 9 && v >= 0 && v < 13)
            {
                if (matEnterrados[u,v] !=1 && matEnterrados[u,v]!=2 && matEnterrados[u,v]!=3)
                {
                        //CON EL 3 MARCO QUE ESA POSICIÓN YA SE HA RECORRIDO, PARA NO ENTRAR EN UN BUCLE INFINITO
                        matEnterrados[u, v] = 3;
                        if (u == posYbandera && v == posXbandera)
                        {
                            matEnterrados[u, v] = 8;
                            exito = true;
                        }
                        else
                        {
                            backTrackingCaminoPosible(u, v);
                        }
                }
            }
            posMov++;
        }
    }


    private void CargarInfoJugadores()
    {
        pers1 = PlayerPrefs.GetInt(persJug1);

        pers2 = PlayerPrefs.GetInt(persJug2);

        pers3 = PlayerPrefs.GetInt(persJug3);

        pers4 = PlayerPrefs.GetInt(persJug4);

        pers5 = PlayerPrefs.GetInt(persJug5);

        pers6 = PlayerPrefs.GetInt(persJug6);

        pers6 = PlayerPrefs.GetInt(persJug6);
        Debug.Log(pers1 + " " + pers2 + " " + pers3 + " " + pers4 + " " + pers5 + " " + pers6);

        nJugadores = PlayerPrefs.GetInt(numJugadoresString);
    }

    public void SiguienteRondaUnSoloJugador()
    {
        Instantiate(sonidoClick);
        Debug.Log("Esta entrando");
        cuentaAtrasTerminada = false;
        cuentaAtras = 5;
        matEnterrados = new int[9, 13];
        ronda++;
        exito = false;
        vivo = true;
        primerMovimiento = true;
        puntosTot += puntos;
        textoRonda.text = "ROUND " + ronda;
        textoPuntosTotales.text = "POINTS " + puntosTot;
        puntos = 0;
        banderaAlcanzada = false;
        Destroy(jugador);
        GameObject pinA = GameObject.FindGameObjectWithTag("pinchosAbiertos");
        Destroy(pinA);

        GameObject[] bandera = GameObject.FindGameObjectsWithTag("bandera");

        foreach (GameObject objeto in bandera)
        {
            Destroy(objeto);
        }


        GameObject[] suelo = GameObject.FindGameObjectsWithTag("Suelo");

        foreach (GameObject objeto in suelo)
        {
            Destroy(objeto);
        }


        GameObject[] objetos = GameObject.FindGameObjectsWithTag("piece");

        foreach (GameObject objeto in objetos)
        {
            Destroy(objeto);
        }

        GameObject[] agujeros = GameObject.FindGameObjectsWithTag("agujero");

        foreach (GameObject objeto in agujeros)
        {
            Destroy(objeto);
        }

        GameObject animalMuerto = GameObject.FindGameObjectWithTag("muerto");
        Destroy(animalMuerto);

        GameObject sonido = GameObject.FindGameObjectWithTag("sonido");
        Destroy(sonido);


        this.InstanciaBandera();
        this.InstanciaJugador();
        this.generarPosicionTrampas();
        this.GenerarPosicionPuntos();
        this.instanciaObjetosEnPantalla();
        cartelMuerto.SetActive(false);
        cartelBandera.SetActive(false);
        cuentaAtrasDespuesMorir = 4;
    }

    public void LoadMainMenu()
    {
        //CAMBIAMOS DE ESCENA, POR EL MOMENTO LA CAMBIAMOS AL MENÚ, PERO HAY QUE HACER UNA ESCENA FINAL
        Instantiate(sonidoClick);
        SceneManager.LoadScene("MainMenu");
    }

    private void InstanciaSonidoExplosion()
    {
        System.Random rand = new System.Random();

        if(rand.Next(0, 2) == 0)
        {
            Instantiate(sonidoExplosion1);
        }
        else
        {
            Instantiate(sonidoExplosion2);
        }
    }

    private void InstanciaMusicaDeFondo()
    {
        System.Random rand = new System.Random();

        switch (rand.Next(1, 6))
        {
            case 1:
                Instantiate(musicaFondo1);
                break;
            case 2:
                Instantiate(musicaFondo2);
                break;
            case 3:
                Instantiate(musicaFondo3);
                break;
            case 4:
                Instantiate(musicaFondo4);
                break;
            case 5:
                Instantiate(musicaFondo5);
                break;
        }
    }

    public void EscapePulsado()
    {
        menuPausa.SetActive(true);
    }

    public void CerrarMenuPausa()
    {
        menuPausa.SetActive(false);
    }

    public void MenuPausaExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
