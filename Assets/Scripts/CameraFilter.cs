using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFilter : MonoBehaviour
{
    [SerializeField]
    private Material material = null;
    [SerializeField]
    private Camera secondCamera = null;

    private RenderTexture secondCameraRt;

    private void Awake()
    {
        secondCameraRt = RenderTexture.GetTemporary(Screen.width, Screen.height, 24);
        secondCamera.targetTexture = secondCameraRt;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetTexture("_MainTex1", source);
        Graphics.Blit(secondCameraRt, destination, material);
    }
}
