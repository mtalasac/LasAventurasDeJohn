using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
   public GameObject John;

    void Update()
    {   
        if (John!=null)
        {
            Vector3 positionCamera = transform.position;
            positionCamera.x = John.transform.position.x;

            transform.position = positionCamera;
        }
        
    }
}
