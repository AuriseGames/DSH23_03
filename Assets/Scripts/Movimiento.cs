using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{

    public Camera camara;
    public int velocidad;
    public GameObject prefabSuelo;

    private Vector3 offset;
    private float valX;
    private float valZ;
    private Rigidbody rb;
    private Vector3 direccionActual;

    // Start is called before the first frame update
    void Start()
    {
        offset = camara.transform.position;
        valX = 0.0f;
        valZ = 0.0f;
        rb = GetComponent<Rigidbody>();
        SueloInicial();
        direccionActual = Vector3.forward;
    }

    void SueloInicial()
    {
        for (int n = 0; n < 3; n++)
        {
            valZ += 6.0f;
            GameObject elSuelo = Instantiate(prefabSuelo, new Vector3(valX, 0.0f, valZ), Quaternion.identity) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        camara.transform.position = this.transform.position + offset;

        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(direccionActual == Vector3.forward)
                direccionActual = Vector3.right;
            
            else
                direccionActual = Vector3.forward;
        }
        float tiempo = velocidad * Time.deltaTime;
        rb.transform.Translate(direccionActual * tiempo);
    }
}
