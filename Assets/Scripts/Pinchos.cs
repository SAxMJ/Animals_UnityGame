using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinchos : MonoBehaviour
{
    public bool pinchosAbiertos = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTruePinchosAbiertos()
    {
        pinchosAbiertos = true;
        Debug.Log("scrPin" + pinchosAbiertos);
    }
}
