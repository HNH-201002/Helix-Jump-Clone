using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavious : MonoBehaviour
{
    private bool hasCollide = false;
    Rigidbody rb;
    [SerializeField] private float force = 5.0f;
    Helix helix;
    public AudioSource audioSource;
    public AudioClip deadAudio;
    public AudioClip groundingAudio;
    public AudioClip brokenHelixAudio;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.tag == "Cylinder" && GameManager.instance.isDead == false || collision.gameObject.tag == "SpawnNewHelix" && GameManager.instance.isDead == false)
        {
            if (hasCollide == false)
            {
                hasCollide = true;
                audioSource.PlayOneShot(groundingAudio);
                rb.AddForce(Vector3.up * force * Time.deltaTime, ForceMode.Impulse);
                Physics.gravity = new Vector3(0, -Mathf.Sqrt(force / 4 * 9.81f), 0);
                Invoke("SetHasCollide",0.1f);
            }
        }
        if (collision.gameObject.tag == "DeadGround")
        {
            audioSource.PlayOneShot(deadAudio);
            GameManager.instance.GameOver();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpawnNewHelix")
        {
            audioSource.PlayOneShot(brokenHelixAudio);
            GameManager.instance.score++;
            Destroy(other.transform.parent.gameObject,1.0f);
            helix = GameManager.FindObjectOfType<Helix>().GetComponent<Helix>();
            helix.SpawnHelix();
        }
        if (other.gameObject.tag == "Cylinder")
        {
            audioSource.PlayOneShot(brokenHelixAudio);
            GameManager.instance.score++;
            Destroy(other.transform.parent.gameObject,1.0f);
        }
    }
    void SetHasCollide()
    {
        hasCollide = false;
    }
   
}
