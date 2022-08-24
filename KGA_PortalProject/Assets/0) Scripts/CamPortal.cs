using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPortal : MonoBehaviour
{
    public Transform PlayerCamera;
    public Transform Portal;
    public Transform OtherPortal;

    void Start()
    {
        
    }

    void Update()
    {
        float angularDifferentBetweenPortalRotations = Quaternion.Angle(Portal.rotation, OtherPortal.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferentBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * PlayerCamera.forward;
        this.transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

        Vector3 playerOffsetFromPortal =  PlayerCamera.position - OtherPortal.position;
        this.transform.position = Portal.position + playerOffsetFromPortal;
    }

}
