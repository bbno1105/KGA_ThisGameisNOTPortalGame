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
