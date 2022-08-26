using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator animDoor;

    void Start()
    {
        animDoor = this.GetComponent<Animator>();
        animDoor.SetBool("isOpen", false);
    }

    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animDoor.SetBool("isOpen", false);
        }
    }
}
