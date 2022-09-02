using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    AudioSource audio;
    Rigidbody rigid;

    float maxVelocity;

    private void Start()
    {
        audio = this.GetComponent<AudioSource>();
        rigid = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(maxVelocity < rigid.velocity.magnitude)
        {
            maxVelocity = rigid.velocity.magnitude;
        }

        if (rigid.velocity.y < -10f)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, -10f, rigid.velocity.z);
        }

        if (this.transform.position.y < -20f)
        {
            this.transform.position = this.transform.parent.position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (maxVelocity > 3f)
        {
            audio.Play();
            maxVelocity = 0;
        }
    }
}
