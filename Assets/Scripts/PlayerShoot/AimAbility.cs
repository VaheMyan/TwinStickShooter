using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAbility : MonoBehaviour, IAbility
{
    [HideInInspector] public Vector3 mousePosition;
    [HideInInspector] public RectTransform crosshairs;

    private Camera cameraMain;

    private void Start()
    {
        cameraMain = Camera.main;
        crosshairs = GameObject.FindGameObjectWithTag("Aim").GetComponent<RectTransform>();
    }

    public void Execute()
    {
        Vector3 crosshairScreenPosition = crosshairs.transform.position;
        Ray ray = cameraMain.ScreenPointToRay(crosshairScreenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = hit.point - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.y = targetRotation.eulerAngles.y;

            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}
