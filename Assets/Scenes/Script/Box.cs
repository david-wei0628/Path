using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject box;
    public Camera CA;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButton(1))
        {
            ////transform.RotateAround(box.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 10);
            //CA.transform.RotateAround(this.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 10);
            ////transform.RotateAround(box.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 10);
            //CA.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 10);
            box.transform.LookAt(this.transform.position);
        }
    }
}
