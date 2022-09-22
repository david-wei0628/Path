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
    // Start is called before the first frame update
    void Start()
    {
        CamearTrans.transform.position = new Vector3(PlayTrans.position.x, PlayTrans.position.y + 4, PlayTrans.position.z - 7);
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
        }

        //if (Input.GetMouseButtonUp(1))
        //{
        //    CamerTrans();
        //}

        if (Input.GetAxis("Vertical") != 0)
        {
            //this.transform.Translate(0, 0, Input.GetAxis("Vertical") * MoveSpeed);
            if (Input.GetAxis("Vertical") > 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.transform.Translate(0, 0, Input.GetAxis("Vertical") * MoveSpeed);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
                this.transform.Translate(0, 0, -1 * Input.GetAxis("Vertical") * MoveSpeed);
            }
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            //this.transform.Translate(Input.GetAxis("Horizontal") * MoveSpeed, 0, 0);
            if(Input.GetAxis("Horizontal") > 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 90, 0);
                this.transform.Translate(0, 0, Input.GetAxis("Horizontal") * MoveSpeed);
            }
            else if(Input.GetAxis("Horizontal") < 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 270, 0);
                this.transform.Translate(0, 0, -1 * Input.GetAxis("Horizontal") * MoveSpeed);
            }
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
            PlayMove();
        }
    }

    void PlayMove()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, maps, 0.1f);

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
        CamearTrans.transform.LookAt(this.transform.position);
        CamearTrans.transform.localEulerAngles = new Vector3(CamearTrans.transform.localEulerAngles.x -10 , CamearTrans.transform.localEulerAngles.y, 0);
        //CamerTrans();
        //上下範圍 X軸 40~-10
        //Y,Z做導向,Z平行於場景  X 做指向

    }

    void CamerTrans()
    {
        CamearTrans.transform.LookAt(this.transform.position);
        CamearTrans.transform.rotation = Quaternion.Euler(CameTrans.localEulerAngles.x - 10, CameTrans.localEulerAngles.y, CameTrans.localEulerAngles.z);        
    }
}
