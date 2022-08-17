using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("캐릭터")]
    [SerializeField] Transform player;

    [Header("카메라")]
    [SerializeField] private Transform playerCamera;

    Vector2 moveInput;

    // TEST
    public float jumpForce;
    public float moveSpeed;
    Rigidbody rigid;
    //

    void Start()
    {
        this.rigid = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        InputKey();
        Move();
    }

    void InputKey()
    {
        this.moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Move()
    {

        Vector3 lookForward = new Vector3(playerCamera.forward.x, 0f, playerCamera.forward.z).normalized;
        Vector3 lookRight = new Vector3(playerCamera.right.x, 0f, playerCamera.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

        // this.player.forward = lookForward;
        this.player.position += moveDir * Time.deltaTime * moveSpeed;
    }

    void Jump()
    {
        rigid.velocity = Vector3.up * jumpForce;
    }

}
