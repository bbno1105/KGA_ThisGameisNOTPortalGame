using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour 
{
    public Transform Player;
    public Transform reciever;

    bool playerIsOverlapping = false;

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
                
            }
            playerIsOverlapping = false;
        }
    }

    void OnTriggerEnter(Collider _other)
    {
        if(_other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider _other)
    {
        if (_other.tag == "Player")
        {
            playerIsOverlapping = false;

        }
    }
}
