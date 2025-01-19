using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    public Canvas canvas;

    // Update is called once per frame
    void Update()
    {
        // Make the UI look face the camera, the camera is isometric so we need to rotate the UI to face the camera
        canvas.transform.LookAt(Camera.main.transform);
    }
}
