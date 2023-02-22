using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pulsar : MonoBehaviour
{

    private Button boton;
    public Image imagen;
    public Sprite[] spNumeros;
    public Text texto;
    public string[] textos;

    private bool contar;
    private int numero;

    // Start is called before the first frame update
    void Start()
    {
        contar = false;
        numero = 3;
        boton = GameObject.FindWithTag("botonpulsar").GetComponent<Button>();
        boton.onClick.AddListener(Pulsado); // manejo de eventos de pulsar
    }
    
    void Pulsado()
    {
        Debug.Log("Pulsado");
        imagen.gameObject.SetActive(true);
        texto.gameObject.SetActive(true);
        boton.gameObject.SetActive(false);
        contar = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (contar)
        {
            if(numero > 0)
            {
                imagen.sprite = spNumeros[numero-1];
                texto.text = textos[numero-1];
                StartCoroutine("Esperar");
                contar = false;
                numero--;
            }
        }
    }

    IEnumerator Esperar() // corrutina
    {
        yield return new WaitForSeconds(1);
        contar = true;
    }

}
