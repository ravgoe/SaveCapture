using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    // Diese Werte sind spezifisch für die Kamera und müssen daher dynamisch gesetzt werden.
    public static bool IsVisibleFromCamera(Camera camera, Collider target)
    {
        float viewRadius = camera.farClipPlane; // Verwende die maximale Sichtweite der Kamera
        float viewAngle = camera.fieldOfView; // Verwende den Sichtwinkel der Kamera

        Collider[] targetsInViewRadius = Physics.OverlapSphere(camera.transform.position, viewRadius);
        Vector3 dirToTarget = (target.transform.position - camera.transform.position).normalized;
        
        if (Vector3.Angle(camera.transform.forward, dirToTarget) < viewAngle / 2)
        {
            float dstToTarget = Vector3.Distance(camera.transform.position, target.transform.position);
            if (dstToTarget <= viewRadius)
            {
                if (Physics.Raycast(camera.transform.position, dirToTarget, out RaycastHit hit) && hit.transform==target.GetComponentInParent<Transform>())
                {
                    return true;
                }
            }
        }

        return false;
    }
}