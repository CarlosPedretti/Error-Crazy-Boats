using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraBarco : MonoBehaviour
{
    public GameObject CameraTarget;

    void Start()
    {
        
    }


    void Update()
    {
        transform.position = CameraTarget.transform.position;
    }
}
