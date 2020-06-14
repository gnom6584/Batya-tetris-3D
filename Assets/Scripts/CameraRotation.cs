using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform Target;

    public void LateUpdate()
    {
        transform.RotateAround(Target.position, transform.up, 10 * (Input.GetAxis("Mouse X")));
        transform.RotateAround(Target.position, Vector3.right, 10 *(Input.GetAxis("Mouse Y")));
        transform.LookAt(Target);
    }
}
