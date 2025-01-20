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
         Quaternion cameraRotation = Camera.main.transform.rotation;
        // optionally ignore all but the y rotation, if you want it to be "square on" to the camera comment out the next line
        cameraRotation = Quaternion.Euler(cameraRotation.x, cameraRotation.eulerAngles.y, cameraRotation.z);
        transform.rotation = cameraRotation;        
    }
}
