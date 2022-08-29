using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRT : MonoBehaviour
{
    [SerializeField] GameObject crtOff;
    [SerializeField] GameObject crtOn;

    [SerializeField] Light crtLight;

    void Start()
    {
        ChangeCRT(GameManager.Instance.isGameClear);
    }

    void ChangeCRT(bool isClear)
    {
        crtOff.SetActive(isClear);
        crtOn.SetActive(!isClear);

        if(isClear)
        {
            crtLight.range = 15f;
        }
        else
        {
            crtLight.range = 2.5f;
        }
    }
}
