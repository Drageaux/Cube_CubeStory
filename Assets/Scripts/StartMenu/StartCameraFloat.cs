using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraFloat : MonoBehaviour
{

    public GameObject Camera;
    //public Rigidbody rd;
    public float Rotatespeed = 0.3f;

    private Camera camera;

    private void Start()
    {
        camera = GetComponentInChildren<Camera>();

    }
    private void Update()
    {
        float X = Input.GetAxis("Mouse X") * Rotatespeed;
        float Y = Input.GetAxis("Mouse Y") * Rotatespeed;

        //camera.transform.localRotation = camera.transform.localRotation * Quaternion.Euler(-Y, 0, 0);
        transform.localRotation = transform.localRotation * Quaternion.Euler(0, X, 0);

    }
}
