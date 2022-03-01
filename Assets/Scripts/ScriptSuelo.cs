using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSuelo : MonoBehaviour
{
    public GameObject sonidoHuesos1;
    public GameObject sonidoHuesos2;
    public GameObject sonidoHuesos3;
    public GameObject sonidoHuesos4;
    public GameObject sonidoHuesos5;
    public GameObject sonidoHuesos6;
    public GameObject sonidoHuesos7;
    public GameObject sonidoHuesos8;
    public GameObject sonidoHuesos9;

    int countPieces = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "piece" && countPieces<40)
        {
            countPieces++;
            ReproduceSonido();
        }
    }

    private void ReproduceSonido()
    {
        System.Random rand = new System.Random();

        switch (rand.Next(1, 10))
        {
            case 1:
                Instantiate(sonidoHuesos1);
                break;
            case 2:
                Instantiate(sonidoHuesos2);
                break;
            case 3:
                Instantiate(sonidoHuesos3);
                break;
            case 4:
                Instantiate(sonidoHuesos4);
                break;
            case 5:
                Instantiate(sonidoHuesos5);
                break;
            case 6:
                Instantiate(sonidoHuesos6);
                break;
            case 7:
                Instantiate(sonidoHuesos7);
                break;
            case 8:
                Instantiate(sonidoHuesos8);
                break;
            case 9:
                Instantiate(sonidoHuesos9);
                break;
        }
    }
}
