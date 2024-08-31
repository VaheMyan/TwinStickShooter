using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraMain;

    private void Start()
    {
        cameraMain = Camera.main.transform;
        PerUpdate();
    }
    public async void PerUpdate()
    {
        while (true)
        {
            if (this == null) return;
            transform.LookAt(transform.position + cameraMain.forward);
            await Task.Delay(10);
        }
    }
}
