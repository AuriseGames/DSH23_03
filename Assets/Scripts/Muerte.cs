using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Muerte : MonoBehaviour
{

    private GameObject player;
    public GameObject explosion;
    public GameObject trail;

    public GameObject sonicDeath;
    public AudioSource jukebox;

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
            sonicDeath.SetActive(true);
            sonicDeath.GetComponent<Rigidbody>().AddForce(Vector3.up * 200f);
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
            jukebox.Stop();
            StartCoroutine("WaitAndLoadGameOver");
        }
    }
    IEnumerator WaitAndLoadGameOver()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
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
