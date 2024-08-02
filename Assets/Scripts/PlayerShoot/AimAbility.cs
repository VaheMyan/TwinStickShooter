using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAbility : MonoBehaviour, IAbility
{
    public Camera cameraMain;
    public RectTransform crosshairs;

    [HideInInspector]public Vector3 mousePosition;

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
