using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muerte : MonoBehaviour
{

    private GameObject player;
    public GameObject explosion;
    public GameObject trail;
    private int groundControl;
    private bool muerto;

    void Start()
    {
        groundControl = -1;
        muerto = false;
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (groundControl == 0 && !muerto)
        {
            muerto = true;
            Debug.Log("Muerto");
            explosion.GetComponent<ParticleSystem>().Play();
            player.GetComponent<Movimiento>().enabled = false;
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            player.GetComponent<MeshRenderer>().enabled = false;
            trail.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "suelo")
        {
            if (groundControl == -1)
            {
                trail.GetComponent<TrailRenderer>().emitting = true;
                groundControl = 1;
            }
            else
            {
                groundControl++;
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "suelo")
        {
            groundControl--;
        }
    }

}
