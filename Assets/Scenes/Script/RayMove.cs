using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMove : MonoBehaviour
{
    public Transform PlayTrans;
    public Transform CameTrans;
    public Camera CamearTrans;
    private Vector3 offset;
    float distance = 0;
    float MoveSpeed;
    Ray ray;
    RaycastHit hit;
    Vector3 maps;
    //bool movelimit = true;

    // Start is called before the first frame update
    void Start()
    {
        CamearTrans.transform.position = new Vector3(PlayTrans.position.x, PlayTrans.position.y + 4, PlayTrans.position.z - 7);
        this.transform.localEulerAngles = new Vector3(0,0,0);
        offset = CameTrans.position - PlayTrans.position;
        MoveSpeed = Time.deltaTime * 5;
        CameTrans.position = offset + PlayTrans.position;
        CamerTrans();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            ScrollView();
        }
        if (Input.GetMouseButtonDown(0))
        {
            SelectRay();
        }

        if (Input.GetMouseButton(1))
        {
            CameRat();
            Debug.Log(Input.GetAxis("Mouse Y"));
        }

        if(Input.GetKey(KeyCode.Z))
        {
            Debug.Log(CamearTrans.transform.localEulerAngles);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            this.transform.Translate(0, 0, Input.GetAxis("Vertical") * MoveSpeed);
            //if (Input.GetAxis("Vertical") > 0)
            //{
            //    this.transform.rotation = Quaternion.Euler(0, 0, 0);
            //    CamearTrans.transform.localEulerAngles = new Vector3(InitCoor.x, InitCoor.y, InitCoor.z);
            //    CamerTrans();
            //    this.transform.Translate(0, 0, Input.GetAxis("Vertical") * MoveSpeed);
            //}
            //else if (Input.GetAxis("Vertical") < 0)
            //{
            //    this.transform.rotation = Quaternion.Euler(0, 180, 0);
            //    CamerTrans();
            //    CamearTrans.transform.localEulerAngles = new Vector3(InitCoor.x, InitCoor.y, InitCoor.z);
            //    this.transform.Translate(0, 0, -Input.GetAxis("Vertical") * MoveSpeed);
            //}
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            this.transform.Translate(Input.GetAxis("Horizontal") * MoveSpeed, 0, 0);
            //if(Input.GetAxis("Horizontal") > 0)
            //{
            //    this.transform.rotation = Quaternion.Euler(0, 90, 0);
            //    this.transform.Translate(0, 0, Input.GetAxis("Horizontal") * MoveSpeed);
            //}
            //else if(Input.GetAxis("Horizontal") < 0)
            //{
            //    this.transform.rotation = Quaternion.Euler(0, 270, 0);
            //    this.transform.Translate(0, 0, -1 * Input.GetAxis("Horizontal") * MoveSpeed);
            //}
        }


    }

    void ScrollView()
    {
        offset = CameTrans.position - PlayTrans.position;
        distance = offset.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * 10;  
        if (distance > 26)
        {   
            distance = 26;
        }
        if (distance < 5)
        {    
            distance = 5;
        }
        offset = offset.normalized * distance;
        CamearTrans.transform.position = offset + PlayTrans.position;
    }

    void SelectRay()
    {
        ray = CamearTrans.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 3500);
        maps = hit.point;
        maps.y = this.transform.position.y;
        Debug.DrawLine(CamearTrans.transform.position, hit.transform.position, Color.blue, 0.5f, true);
        //return Physics.Raycast(ray, out hit, 3500);
        if (Vector3.Distance(this.transform.position, maps) < 100f)
        {
            //this.transform.LookAt(maps);
           // CamearTrans.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + 20, -transform.localEulerAngles.y, 0);
            PlayMove();
        }
    }

    void PlayMove()
    {
        this.transform.position = Vector3.Slerp(this.transform.position, maps, 0.1f);
        //this.transform.LookAt(maps);
        if (Vector3.Distance(this.transform.position, maps) > 0.1f )
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            { 
                Invoke("PlayMove", 0.05f); 
            }
            else
            {
                return;
            }
        }
    }

    void CameRat()
    {
        CamearTrans.transform.RotateAround(this.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 5);
        CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 5);

        //if (CamearTrans.transform.localEulerAngles.x >= 40)
        //{
        //    if (Input.GetAxis("Mouse Y") > 0)
        //    {
        //        CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 5);
        //    }
        //    else
        //    {
        //        CamearTrans.transform.localEulerAngles = new Vector3(30, CamearTrans.transform.localEulerAngles.y, 0);
        //    }
        //}
        //else if (CamearTrans.transform.localEulerAngles.x <= -10)
        //{
        //    if (Input.GetAxis("Mouse Y") < 0)
        //    {
        //        CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 5);
        //    }
        //    else
        //    {
        //        CamearTrans.transform.localEulerAngles = new Vector3(-5, CamearTrans.transform.localEulerAngles.y, 0);
        //    }
        //}
        //else if (CamearTrans.transform.localEulerAngles.x > -10 && CamearTrans.transform.localEulerAngles.x < 40)
        //{
        //    CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 5);
        //}
        CamerTrans();
        //上下範圍 X軸 40~-10
        //Y,Z做導向,Z平行於場景  X 做指向

    }

    void CamerTrans()
    {
        CamearTrans.transform.LookAt(this.transform.position);
        CamearTrans.transform.localEulerAngles = new Vector3(CameTrans.localEulerAngles.x-20, CameTrans.localEulerAngles.y, 0);        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision + "1");
    //    //movelimit = false;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other + "2");
    //}
}
