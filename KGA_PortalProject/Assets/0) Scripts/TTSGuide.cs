using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSGuide : MonoBehaviour
{
    [TextArea]
    [SerializeField] string TxtGuide;

    bool isTrigger;

    void Start()
    {
        isTrigger = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (isTrigger) return;

        if(other.tag == "Player")
        {
            TTS.Instance.TTSPlay(TxtGuide);
            isTrigger = true;
        }
    }
}
