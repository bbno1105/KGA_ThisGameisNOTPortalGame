// #define OCULUS
#define UNITY

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class PlayerManager : MonoBehaviour
{
    [Header("캐릭터")]
    [SerializeField] Transform player;
    Vector2 playerInput;

    bool isPick;
    float objectDistance;
    float objectRadius;
    Vector3 objectScale;
    GameObject pickObject;
    bool canJump;
    // TEST
    public float jumpForce;
    public float moveSpeed;
    Rigidbody rigid;
    //

    [Header("카메라")]
    [SerializeField] Transform playerCamera;
    [SerializeField] float CameraSeneitivity;
    [SerializeField] float CameraRotationLimit;
    [SerializeField] CameraAim cameraAim;

    Vector2 cameraInput;
    float currentCameraRotationX;


    void Start()
    {
        this.rigid = player.GetComponent<Rigidbody>();
        canJump = true;
    }

    void Update()
    {
#if UNITY
        KeyInput(); // 키보드 인풋
#endif

#if OCULUS
        OculusInputData(); // 오큘러스 인풋
#endif

        PlayerMove(); // 이동
        PlayerRotation(); // 좌우
        CameraMove(); // 상하
        Pick();
        JumpRay();


        if(rigid.velocity.y < -10f)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, -10f, rigid.velocity.z);
        }
    }

    void KeyInput()
    {
        this.playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        this.cameraInput = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!isPick && cameraAim.isHitObject)
            {
                isPick = true;
                pickObject = cameraAim.hitObject;
                objectDistance = (cameraAim.hitObject.transform.position - playerCamera.transform.position).magnitude;
                objectScale = cameraAim.hitObject.transform.localScale;
                objectRadius = (cameraAim.hitObject.transform.position - cameraAim.hitRay.point).magnitude;
                if(pickObject.GetComponent<BoxCollider>() != null)
                {
                    pickObject.GetComponent<BoxCollider>().enabled = false;
                }
                else if(pickObject.GetComponent<SphereCollider>() != null)
                {
                    pickObject.GetComponent<SphereCollider>().enabled = false;
                }
                pickObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            else if(isPick) // 놓는다
            {
                isPick = false;
                if (pickObject.GetComponent<BoxCollider>() != null)
                {
                    pickObject.GetComponent<BoxCollider>().enabled = true;
                }
                else if (pickObject.GetComponent<SphereCollider>() != null)
                {
                    pickObject.GetComponent<SphereCollider>().enabled = true;
                }
                pickObject.GetComponent<Rigidbody>().isKinematic = false;
            }

        }
    }

    void OculusInput()
    {
        this.playerInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        if (OVRInput.GetUp(OVRInput.RawButton.A))
        {
            Jump();
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (!isPick && cameraAim.isHitObject)
            {
                isPick = true;
                pickObject = cameraAim.hitObject;
                objectDistance = cameraAim.wallDistance;
                objectScale = cameraAim.hitObject.transform.localScale;
                objectRadius = (cameraAim.hitObject.transform.position - cameraAim.hitRay.point).magnitude;
                if (pickObject.GetComponent<BoxCollider>() != null)
                {
                    pickObject.GetComponent<BoxCollider>().enabled = false;
                }
                else if (pickObject.GetComponent<SphereCollider>() != null)
                {
                    pickObject.GetComponent<SphereCollider>().enabled = false;
                }
                pickObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            else if (isPick) // 놓는다
            {
                isPick = false;
                if (pickObject.GetComponent<BoxCollider>() != null)
                {
                    pickObject.GetComponent<BoxCollider>().enabled = true;
                }
                else if (pickObject.GetComponent<SphereCollider>() != null)
                {
                    pickObject.GetComponent<SphereCollider>().enabled = true;
                }
                pickObject.GetComponent<Rigidbody>().isKinematic = false;
            }

        }
    }

    void PlayerMove()
    {
        Vector3 lookForward = new Vector3( playerCamera.forward.x, 0f, playerCamera.forward.z).normalized;
        Vector3 lookRight = new Vector3(playerCamera.right.x, 0f, playerCamera.right.z).normalized;
        Vector3 moveDir = lookForward * playerInput.y + lookRight * playerInput.x;

        // this.player.forward = lookForward;

        rigid.MovePosition(player.position + moveDir * Time.deltaTime * moveSpeed);
    }

    void Jump()
    {
        rigid.velocity = Vector3.up * jumpForce;
        canJump = false;
    }

    void JumpRay()
    {
        RaycastHit hit; 
        Debug.DrawRay(player.transform.position, Vector3.down * 0.8f, Color.red); 
        if (Physics.Raycast(player.transform.position, Vector3.down, out hit, 0.8f)) 
        { 
            canJump = true; 
            return;
        }
        canJump = false;
    }

    void Pick()
    {
        if(isPick) // test
        {
            float wallDistance = cameraAim.wallDistance;
            Vector3 newScale = objectScale * (wallDistance / objectDistance);
            float newScaleValue = newScale.x;

            newScaleValue = Mathf.Clamp(newScaleValue, 0.05f, 3f);

            pickObject.transform.localScale = new Vector3(newScaleValue, newScaleValue, newScaleValue);

            float newRadius = objectRadius * (newScaleValue / objectScale.z);

            pickObject.transform.position = cameraAim.hitWallRay.point + cameraAim.hitWallRay.normal * newRadius;
            
        }
    }

    void CameraMove()
    {
        // 상하 움직임
        float cameraRotationX = cameraInput.x * CameraSeneitivity;
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -CameraRotationLimit, CameraRotationLimit);

        playerCamera.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    void PlayerRotation() // 좌우
    {
        Vector3 playerRotationY = new Vector3(0f, cameraInput.y, 0f) * CameraSeneitivity;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(playerRotationY));
    }
}
