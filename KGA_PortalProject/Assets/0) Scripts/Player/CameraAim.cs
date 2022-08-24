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

    Vector3 screenCenter;

    void Start()
    {
        screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);

        LayerMask targetLayer = LayerMask.NameToLayer("ActiveObject");
        LayerMask WallLayer = LayerMask.NameToLayer("Wall");

        targetLayMask = 1 << targetLayer.value;
        WallLayMask = 1 << WallLayer.value;
    }

    void Update()
    {
        FindTarget();
        FindWall();
    }

    void FindTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out hitRay,10f, targetLayMask))
        {
            hitObject = hitRay.collider.gameObject;
            hitPos = hitRay.collider.gameObject.transform.position;
            isHitObject = true;
        }
        else
        {
            isHitObject = false;
        }
    }

    void FindWall()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out hitWallRay, 10f, WallLayMask))
        {
            hitWallObject = hitWallRay.collider.gameObject;
            hitWallPos = hitWallRay.collider.gameObject.transform.position;
        }
    }
}
