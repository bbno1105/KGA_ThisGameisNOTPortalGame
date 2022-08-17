using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraMove : MonoBehaviour
{
    public Transform Player;
    public float CamAngleSpeed;

    void Update()
    {
        LookAround();
    }

    public void LookAround()
    {
        float camAngleSpeed = CamAngleSpeed * Time.deltaTime;
        Vector3 cameraRotation = this.transform.rotation.eulerAngles;

        cameraRotation.x -= Input.GetAxis("Mouse Y") * camAngleSpeed * Time.deltaTime;
        cameraRotation.y += Input.GetAxis("Mouse X") * camAngleSpeed * Time.deltaTime;

        if (cameraRotation.x < 180f)
        {
            cameraRotation.x = Mathf.Clamp(cameraRotation.x, -1f, 70f);
        }
        else
        {
            cameraRotation.x = Mathf.Clamp(cameraRotation.x, 300f, 361f);
        }

        Player.transform.localEulerAngles = new Vector3(cameraRotation.x, cameraRotation.y, Player.transform.rotation.eulerAngles.z);
        // camAngle.x - mouseDelta.y << 국내에서 흔하게 사용되는 조작방법으로 사용자에 따라 익숙함이 다를 수 있다
        // +, - 값의 두가지 설정을 두어서 원하는 조작방법으로 조작할 수 있도록 옵션에 추가하자
    }
}



