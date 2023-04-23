using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraMaxChange;
    public Vector2 cameraMinChange;
    public Vector3 playerChange;
    private CameraMovement cam;
    public Transform movePoint;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
    }

    // lorsque le personnage arrive dans une zone de transition entre deux pièces, lui et la caméra sont déplacés 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            cam.minPosition += cameraMinChange;
            cam.maxPosition += cameraMaxChange;
            movePoint.transform.position += playerChange;
            collision.transform.position += playerChange;
        }
    }
}
