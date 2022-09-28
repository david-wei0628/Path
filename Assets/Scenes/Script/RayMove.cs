using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMove : MonoBehaviour
{
    public Transform PlayTrans;
    public Transform CameTrans;
    public Camera CamearTrans;
    public GameObject SeBox;
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
        QualitySettings.vSyncCount = 0;//垂直同步
        Application.targetFrameRate = 100;//FPS禎數

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
        SeBox.transform.localEulerAngles = new Vector3(0, CamearTrans.transform.localEulerAngles.y, 0);
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
            //Debug.Log(Input.GetAxis("Mouse Y"));
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("LocoR" + CamearTrans.transform.localEulerAngles);
            Debug.Log("LocoP" + CamearTrans.transform.localPosition);
            Debug.Log("Rot" + CamearTrans.transform.rotation);
            Debug.Log("Pos" + CamearTrans.transform.position);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            KeyBoardMoveCamera(Input.GetAxis("Vertical"));
            //this.transform.Translate(0, 0, Input.GetAxis("Vertical") * MoveSpeed);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                //this
            }

            KeyBoardMoveCamera(Input.GetAxis("Horizontal"));
            //this.transform.Translate(0, 0, Input.GetAxis("Horizontal") * MoveSpeed);
        }

        if (Input.GetAxis("Jump") != 0 && transform.position.y <= 3)
        {
            this.transform.Translate(0, Input.GetAxis("Jump") * MoveSpeed, 0);
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

        if (Vector3.Distance(this.transform.position, maps) < 100f)
        {
            RayMoveCamera();
            PlayMove();
        }
    }

    void PlayMove()
    {
        this.transform.position = Vector3.Slerp(this.transform.position, maps, 0.1f);
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
        //CamearTrans.transform.RotateAround(this.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 5);
        //CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 5);

        if (Input.GetAxis("Mouse X") != 0)
        {
            CamearTrans.transform.RotateAround(this.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 5);
        }
        else
        {
            if(Input.GetAxis("Mouse Y") < 0)
            {
                if(CamearTrans.transform.localEulerAngles.x > 40 && CamearTrans.transform.localEulerAngles.x < 180)
                {
                    CamearTrans.transform.localEulerAngles = new Vector3(30, CamearTrans.transform.localEulerAngles.y, 0);
                }
                else
                {
                    CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 5);
                }
            }
            else if (Input.GetAxis("Mouse Y") > 0)
            {
                if (CamearTrans.transform.localEulerAngles.x > 340)
                {
                    CamearTrans.transform.localEulerAngles = new Vector3(350, CamearTrans.transform.localEulerAngles.y, 0);
                }
                else
                {
                    CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 5);
                }
                //CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 5);
                //Debug.Log(CamearTrans.transform.localEulerAngles.x);
            }
        }
        
        CamerTrans();
        //上下範圍 X軸 40~-10
        //Y,Z做導向,Z平行於場景  X 做指向
    }

    void CamerTrans()
    {
        CamearTrans.transform.LookAt(this.transform.position);
        //Debug.Log(CamearTrans.transform.localEulerAngles.x);
        if(CamearTrans.transform.localEulerAngles.x > 300)
        {
            CamearTrans.transform.localEulerAngles = new Vector3(CameTrans.localEulerAngles.x + 20, CameTrans.localEulerAngles.y, 0);
        }
        else
        {
            CamearTrans.transform.localEulerAngles = new Vector3(CameTrans.localEulerAngles.x - 20, CameTrans.localEulerAngles.y, 0);        
        }
        //Debug.Log(CamearTrans.transform.localEulerAngles.x);
    }

    void RayMoveCamera()
    {
        Vector3 InitCoor = CamearTrans.transform.position;
        this.transform.LookAt(maps);
        CamearTrans.transform.position = InitCoor;
        CamerTrans();
    }

    void KeyBoardMoveCamera(float move)
    {
        Vector3 InitCoor = CamearTrans.transform.position;
        this.transform.Translate(0, 0, move * MoveSpeed);
        CamearTrans.transform.position = InitCoor;
        CamerTrans();
    }
}
