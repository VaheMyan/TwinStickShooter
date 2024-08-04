using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour, IUpdate
{
    private Transform cameraMain;

    private void Start()
    {
        cameraMain = Camera.main.transform;
    }
    public void PerUpdate()
    {
        transform.LookAt(transform.position + cameraMain.forward);
    }
}
