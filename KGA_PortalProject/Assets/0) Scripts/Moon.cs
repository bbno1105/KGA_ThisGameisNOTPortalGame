using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < -20f)
        {
            this.transform.position = new Vector3(-25, 40, 0);
            this.transform.localScale = new Vector3(3, 3, 3);
            this.rigid.isKinematic = true;
        }
    }
}
