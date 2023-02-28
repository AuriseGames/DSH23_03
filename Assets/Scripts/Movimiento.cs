using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{

    public Camera camara;
    public float velocidad;
    public float puntos;
    public GameObject prefabSuelo;
    public GameObject prefabRing;

    public AudioClip ring;

    private Vector3 offset;
    private float valX;
    private float valZ;
    private Rigidbody rb;
    private Vector3 direccionActual;
    public Text texto;

    private bool start;

    int contadorSuelosSeguidos = 0;

    // Start is called before the first frame update
    void Start()
    {
        puntos = 0;
        texto.text = puntos.ToString("0");
        start = false;
        velocidad = 0f;
        offset = camara.transform.position;
        valX = 0.0f;
        valZ = 0.0f;
        rb = GetComponent<Rigidbody>();
        SueloInicial();
        direccionActual = Vector3.forward;
    }

    void SueloInicial()
    {
        for (int n = 0; n < 12; n++)
        {
            valZ += 6.0f;
            GameObject elSuelo = Instantiate(prefabSuelo, new Vector3(valX, 0.0f, valZ), Quaternion.identity) as GameObject;
            GameObject ring = Instantiate(prefabRing, new Vector3(valX, 4.0f, valZ), prefabRing.transform.rotation) as GameObject;
            ring.transform.parent = elSuelo.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        camara.transform.position = this.transform.position + offset;
        // UpdatePuntos();
        if (start && Input.GetKeyUp(KeyCode.Space))
        {
            if (direccionActual == Vector3.forward)
                direccionActual = Vector3.right;

            else
                direccionActual = Vector3.forward;
        }
        float tiempo = velocidad * Time.deltaTime;
        rb.transform.Translate(direccionActual * tiempo);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!start && other.gameObject.tag == "suelo")
        {
            velocidad = 10f;
            start = true;
        }
    }

    // private void UpdatePuntos()
    // {
    //     puntos += Time.deltaTime;
    //     // texto.text = "Puntos: " + puntos.ToString("0");
    //     texto.text = puntos.ToString("0");
    // }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "ring")
        {
            AudioSource.PlayClipAtPoint(ring, transform.position, 100.0f);
            Destroy(other.gameObject);
            velocidad += 0.5f;
            puntos += 1;
            texto.text = puntos.ToString("0");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "suelo")
        {
            // velocidad += 0.1f;
            //Obtenemos un numero aleatorio entre 0 y 2
            int num = Random.Range(0, 2);
            //Segun el numero aleatorio, cambiamos la posicion del suelo
            switch (num)
            {
                case 0:
                    valX += 6.0f;
                    valZ -= 6.0f;
                    break;
                case 1:
                    break;
            }
            contadorSuelosSeguidos--;
            valZ += 6.0f;
            GameObject elSuelo = Instantiate(prefabSuelo, new Vector3(valX, 0.0f, valZ), Quaternion.identity) as GameObject;
            num = Random.Range(0, 2);
            if (num == 0)
            {
                GameObject ring = Instantiate(prefabRing, new Vector3(valX, 4.0f, valZ), prefabRing.transform.rotation) as GameObject;
                ring.transform.parent = elSuelo.transform;
            }
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(other.gameObject, 2.0f);
            
        }
    }

}
