using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPortal : MonoBehaviour
{
    public Transform Player;


    void Start()
    {
        
    }

    void Update()
    {
        Lookfoward();
    }

    void Lookfoward()
    {
        this.transform.forward = Player.transform.forward;
    }
}
