using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenHall : MonoBehaviour
{
    [Header("처음")]
    [SerializeField] GameObject hiddenA;

    [Header("나중")]
    [SerializeField] GameObject hiddenB;

    void changeHidden()
    {
        hiddenA.gameObject.SetActive(false);
        hiddenB.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        changeHidden();
    }
}
