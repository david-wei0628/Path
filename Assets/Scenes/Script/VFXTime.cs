using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXTime : MonoBehaviour
{
    //float StartTime;
    GameObject Play;
    // Start is called before the first frame update
    void Start()
    {
        //StartTime = 10f;        
    }

    private void Awake()
    {
        Play = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //if (StartTime <= 0)
        //{
        //    DeleCube();
        //}
        //else
        //{
        //    StartTime = StartTime - Time.deltaTime;
        //}

        if (Play.GetComponent<Rigidbody>().velocity.magnitude <= 0)
        {
            DeleCube();
        }

        this.transform.Rotate(0, 1, 0);

        if (Input.GetMouseButtonDown(0) || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            DeleCube();
        }
        
    }

    void DeleCube()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
