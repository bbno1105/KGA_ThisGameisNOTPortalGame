using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour 
{
    public Transform Player;

    public Transform[] Dice;
    bool[] DiceIsOverlapping;

    public Transform reciever;

    public GameObject ActivePortal;
    public GameObject ActiveFalsePortal;

    [SerializeField] bool mustActive;

    bool playerIsOverlapping = false;

    private void Start()
    {
        DiceIsOverlapping = new bool[Dice.Length];

        for (int i = 0; i < Dice.Length; i++)
        {
            DiceIsOverlapping[i] = false;
        }
    }

    void Update()
    {
        Vector3 portalToPlayer = Player.position - this.transform.position;
        float dotProduct = Vector3.Dot(this.transform.forward, portalToPlayer);

        // UnityEngine.Debug.Log($"{gameObject.name} : {dotProduct}");

        if (playerIsOverlapping)
        {
            if (dotProduct < 0f)
            {
                float rotationDiffrent = -Quaternion.Angle(this.transform.rotation, reciever.rotation);
                rotationDiffrent += 180f;
                Player.Rotate(Vector3.up, rotationDiffrent);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiffrent, 0f) * portalToPlayer;
                Player.position = reciever.position + positionOffset;

                if(!mustActive)
                {
                    this.transform.parent.GetChild(0).gameObject.SetActive(false);
                    reciever.parent.GetChild(0).gameObject.SetActive(true);

                    if(ActivePortal != null) ActivePortal.SetActive(true);
                    if(ActiveFalsePortal != null) ActiveFalsePortal.SetActive(false);
                }


            }
            playerIsOverlapping = false;
        }





        // dice
        for (int i = 0; i < Dice.Length; i++)
        {
            Vector3 portalToDice = Dice[i].position - this.transform.position;
            float dotProductDice = Vector3.Dot(this.transform.forward, portalToDice);

            if (DiceIsOverlapping[i])
            {
                if (dotProductDice < 0f)
                {
                    float rotationDiffrent = -Quaternion.Angle(this.transform.rotation, reciever.rotation);
                    rotationDiffrent += 180f;
                    Dice[i].Rotate(Vector3.up, rotationDiffrent);

                    Vector3 positionOffset = Quaternion.Euler(0f, rotationDiffrent, 0f) * portalToDice;
                    Dice[i].position = reciever.position + positionOffset;
                }
                DiceIsOverlapping[i] = false;
            }
        }


    }

    void OnTriggerEnter(Collider _other)
    {
        if(_other.tag == "Player")
        {
            playerIsOverlapping = true;
        }

        if (_other.tag == "Dice" || _other.tag == "Moon")
        {
            for (int i = 0; i < Dice.Length; i++)
            {
                if(_other.gameObject == Dice[i].gameObject)
                {
                    DiceIsOverlapping[i] = true;
                    return;
                }
            }
        }

    }

    void OnTriggerExit(Collider _other)
    {
        if (_other.tag == "Player")
        {
            playerIsOverlapping = false;

        }

        if (_other.tag == "Dice" || _other.tag == "Moon")
        {
            for (int i = 0; i < Dice.Length; i++)
            {
                if (_other.gameObject == Dice[i].gameObject)
                {
                    DiceIsOverlapping[i] = false;
                    return;
                }
            }
        }
    }
}
