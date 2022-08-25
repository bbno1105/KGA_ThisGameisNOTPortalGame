using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    bool isOn;

    [SerializeField] GameObject openObject;

    Vector3 onPos;
    Vector3 offPos;

    void Start()
    {
        offPos = this.transform.localPosition;
        onPos = new Vector3(offPos.x, 0.26f, offPos.z);
    }

    void Update()
    {
        if(isOn)
        {
            this.transform.localPosition = Vector3.Lerp(offPos, onPos, 1f);
            openObject.SetActive(false);
        }
        else
        {
            this.transform.localPosition = Vector3.Lerp(onPos, offPos, 1f);
            openObject.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("ActiveObject"))
        {
            isOn = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ActiveObject"))
        {
            isOn = false;
        }
    }
}
