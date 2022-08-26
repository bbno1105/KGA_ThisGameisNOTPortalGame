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
    int WallLayMask;
    int WallLayMask2;

    float wallDistance;
    float targetDistance;

    Vector3 screenCenter;

    void Start()
    {
        screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);

        LayerMask targetLayer = LayerMask.NameToLayer("ActiveObject");
        LayerMask WallLayer = LayerMask.NameToLayer("Wall");
        LayerMask WallLayer2 = LayerMask.NameToLayer("DarkWall");

        targetLayMask = 1 << targetLayer.value;
        WallLayMask = 1 << WallLayer.value;
        WallLayMask2 = 1 << WallLayer2.value;
    }

    void Update()
    {
        FindWall();
        FindTarget();
    }

    void FindTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

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
        if (Physics.Raycast(ray, out hitWallRay, 1000f, WallLayMask))
        {
            Vector3 hitPointPos = hitWallRay.point;
            wallDistance = (Camera.main.gameObject.transform.position - hitPointPos).magnitude;

            hitWallPos = hitWallRay.collider.gameObject.transform.position;
            hitWallObject = hitWallRay.collider.gameObject;
        }
        else if (Physics.Raycast(ray, out hitWallRay, 1000f, WallLayMask2))
        {
            Vector3 hitPointPos = hitWallRay.point;
            wallDistance = (Camera.main.gameObject.transform.position - hitPointPos).magnitude;

            hitWallObject = hitWallRay.collider.gameObject;
            hitWallPos = hitWallRay.collider.gameObject.transform.position;
        }
        else
        {
            wallDistance = 1000000f;
        }
    }
}
