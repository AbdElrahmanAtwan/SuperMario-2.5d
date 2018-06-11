using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{

    public GameObject Player;

    private float xOffset;
    private Camera cam;
    private float xPos;
    private float oldXPos;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        xOffset = transform.position.x - Player.transform.position.x;
        cam = GetComponent<Camera>();
        oldXPos = Player.transform.position.x;
    }
   

    void LateUpdate()
    {
        if (oldXPos< Player.transform.position.x)
        {
            oldXPos = Player.transform.position.x;
            transform.position = new Vector3(Player.transform.position.x + xOffset, transform.position.y, transform.position.z);
        }
       
       
    }
}
