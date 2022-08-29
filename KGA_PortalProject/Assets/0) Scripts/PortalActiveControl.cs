using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActiveControl : MonoBehaviour
{
    GameObject ActiveTruePortal;
    GameObject ActiveFalsePortal;

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            ActiveTruePortal.SetActive(true);
            ActiveFalsePortal.SetActive(false);
        }
    }
}
