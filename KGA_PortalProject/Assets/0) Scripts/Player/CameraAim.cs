using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour
{
    public GameObject hitObject;
    public Vector3 hitPos;
    public RaycastHit hitRay;
    public bool isHitObject;
    int targetLayMask;

    public GameObject hitWallObject;
    public Vector3 hitWallPos;
    public RaycastHit hitWallRay;
    int wallLayMask;
    int portalMask;

    public float wallDistance;
    float targetDistance;

    Vector3 screenCenter;

    void Start()
    {
        screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);

        LayerMask targetLayer = LayerMask.NameToLayer("ActiveObject");
        LayerMask WallLayer = LayerMask.NameToLayer("Wall");
        LayerMask WallLayer2 = LayerMask.NameToLayer("DarkWall");

        targetLayMask = 1 << targetLayer.value;
        wallLayMask = (1 << WallLayer.value) + (1 << WallLayer2.value);
        portalMask = LayerMask.GetMask("Portal");
    }

    void Update()
    {
        FindWall();
        FindTarget();
    }

    void FindTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out hitRay,100000f, portalMask))
        {
            Vector3 rayDirection = hitRay.point - Camera.main.gameObject.transform.position;
            Vector3 hitPortalPoint = hitRay.point - hitRay.collider.gameObject.transform.position;
            Vector3 newPortalPosition = hitRay.collider.GetComponent<PortalTeleporter>().reciever.transform.position;
            ray.origin = newPortalPosition + hitPortalPoint;
            ray.direction = rayDirection;
        }

        if (Physics.Raycast(ray, out hitRay,100000f, targetLayMask))
        {
            Vector3 hitPointPos = hitRay.point;
            targetDistance = (Camera.main.gameObject.transform.position - hitPointPos).magnitude;

            if(targetDistance < wallDistance)
            {
                hitPos = hitRay.collider.gameObject.transform.position;
                hitObject = hitRay.collider.gameObject;
                isHitObject = true;
            }
            else
            {
                isHitObject = false;
            }
        }
        else
        {
            isHitObject = false;
        }
    }

    void FindWall()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        Vector3 startRayPoint = Camera.main.gameObject.transform.position;

        wallDistance = 0;
        float portalDistance = 0;

        if (Physics.Raycast(ray, out hitRay, 1000f, portalMask))
        {
            Vector3 rayDirection = hitRay.point - startRayPoint;
            Vector3 hitPortalPoint = hitRay.point - hitRay.collider.gameObject.transform.position;
            Vector3 newPortalPosition = hitRay.collider.GetComponent<PortalTeleporter>().reciever.transform.position;

            float wallDistanceforPortal = 0;
            RaycastHit hitWallRayforPortal;
            if (Physics.Raycast(ray, out hitWallRayforPortal, 1000f, wallLayMask))
            {
                wallDistanceforPortal = Vector3.Distance(startRayPoint,hitWallRayforPortal.point);
            }

            Vector3 hitPointPos = hitRay.point;
            portalDistance = Vector3.Distance(startRayPoint, hitPointPos);

            if(portalDistance <= wallDistanceforPortal || wallDistanceforPortal == 0)
            {
                wallDistance += Vector3.Distance(startRayPoint, hitPointPos);
                startRayPoint = ray.origin;

                ray.origin = newPortalPosition + hitPortalPoint;
                ray.direction = rayDirection;
            }
        }

        if (Physics.Raycast(ray, out hitWallRay, 1000f, wallLayMask))
        {
            Vector3 hitPointPos = hitWallRay.point;
            wallDistance += Vector3.Distance(startRayPoint, hitPointPos);

            hitWallPos = hitWallRay.collider.gameObject.transform.position;
            hitWallObject = hitWallRay.collider.gameObject;
        }
        //else if (Physics.Raycast(ray, out hitWallRay, 1000f, wallLayMask2))
        //{
        //    Vector3 hitPointPos = hitWallRay.point;
        //    wallDistance += Vector3.Distance(startRayPoint, hitPointPos);

        //    hitWallObject = hitWallRay.collider.gameObject;
        //    hitWallPos = hitWallRay.collider.gameObject.transform.position;
        //}
    }
}
