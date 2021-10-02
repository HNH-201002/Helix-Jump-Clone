using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void LateUpdate()
    {
        if (transform.position.y - player.transform.position.y >3.0f) {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + 3, transform.position.z);
        } 
    }
}