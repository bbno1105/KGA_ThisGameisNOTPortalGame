using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSGuide : MonoBehaviour
{
    [SerializeField] string TxtGuide;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            TTS.Instance.TTSPlay(TxtGuide);
        }
    }
}
