using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera[] Camera;
    public Material[] CameraMat;

    void Start()
    {
        RenderCamera();
    }

    void RenderCamera()
    {
        for (int i = 0; i < Camera.Length; i++)
        {
            if (Camera[i] == null) continue;
            if (Camera[i].targetTexture != null)
            {
                Camera[i].targetTexture.Release();
            }
            Camera[i].targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            CameraMat[i].mainTexture = Camera[i].targetTexture;
        }
    }
}
