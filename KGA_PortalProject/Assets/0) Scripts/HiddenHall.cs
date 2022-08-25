using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenHall : MonoBehaviour
{
    [SerializeField] GameObject[] hidden;

    void changeHidden()
    {
        hidden[0].gameObject.SetActive(false);
        hidden[1].gameObject.SetActive(false);
        hidden[2].gameObject.SetActive(true);
        hidden[3].gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        changeHidden();
    }
}
