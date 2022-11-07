using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCS : MonoBehaviour
{

    public Camera CamearTrans;
    public GameObject MouseVFX;
    Ray ray;
    RaycastHit hit;
    Vector3 maps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectRay();
        }
    }

    void SelectRay()
    {
        ray = CamearTrans.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 3500);
        maps = hit.point;
        //maps.y = this.transform.position.y;
        Debug.DrawLine(CamearTrans.transform.position, hit.transform.position, Color.blue, 1f, true);
        //Debug.Log(maps);
        Instantiate(MouseVFX, maps, new Quaternion(0, 0, 0, 0));
    }
}
