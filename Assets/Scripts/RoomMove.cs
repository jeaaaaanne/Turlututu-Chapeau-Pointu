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
        //cam = Camera.main.GetComponent<CameraMovement>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
    }

    void Update()
    {
        
    }

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
