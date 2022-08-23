using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("캐릭터")]
    [SerializeField] Transform player;
    Vector2 playerInput;

    // TEST
    public float jumpForce;
    public float moveSpeed;
    Rigidbody rigid;
    //

    [Header("카메라")]
    [SerializeField] Transform playerCamera;
    [SerializeField] float CameraSeneitivity;
    [SerializeField] float CameraRotationLimit;
    Vector2 cameraInput;
    float currentCameraRotationX;
    


    void Start()
    {
        this.rigid = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        InputData(); // 인풋
        PlayerMove(); // 이동
        PlayerRotation(); // 좌우
        CameraMove(); // 상하
    }

    void InputData()
    {
        this.playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        this.cameraInput = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
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
    }

    void CameraMove()
    {
        // 상하 움직임
        float cameraRotationX = cameraInput.x * CameraSeneitivity;
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -CameraRotationLimit, CameraRotationLimit);

        playerCamera.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    void PlayerRotation()
    {
        Vector3 playerRotationY = new Vector3(0f, cameraInput.y, 0f) * CameraSeneitivity;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(playerRotationY));
    }
}
