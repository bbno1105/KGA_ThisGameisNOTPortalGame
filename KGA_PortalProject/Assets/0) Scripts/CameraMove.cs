using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraMove : MonoBehaviour
{

    public float CamAngleSpeed;

    void Update()
    {
        LookAround();
    }

    public void LookAround()
    {
        float camAngleSpeed = CamAngleSpeed * Time.deltaTime;
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * camAngleSpeed;
        

        if (mouseDelta.x < 180f)
        {
            mouseDelta.x = Mathf.Clamp(mouseDelta.x, -1f, 70f);
        }
        else
        {
            mouseDelta.x = Mathf.Clamp(mouseDelta.x, 300f, 361f);
        }

        this.transform.rotation = Quaternion.Euler(mouseDelta.x, mouseDelta.x, this.transform.rotation.eulerAngles.z);
        // camAngle.x - mouseDelta.y << 국내에서 흔하게 사용되는 조작방법으로 사용자에 따라 익숙함이 다를 수 있다
        // +, - 값의 두가지 설정을 두어서 원하는 조작방법으로 조작할 수 있도록 옵션에 추가하자
    }
}
