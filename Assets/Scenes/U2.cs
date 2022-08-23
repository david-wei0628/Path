using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U2 : U1
{
    private Transform Way2;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Debug.Log("UUI2");
        Way2 = RaceCircle.GetNextWaypoint(Way2);
        //transform.LookAt(Way2);
    }
    private void Awake()
    {
        
    }
    bool bt = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UU2();
        }
        if (bt)
        {
            transform.position = Vector3.Lerp(transform.position, Way2.position, Time.deltaTime * 10f);
            if (Vector3.Distance(transform.position, Way2.position) < 0.1f)
            {
                Way2 = RaceCircle.GetNextWaypoint(Way2);
            }
        }

    }
    protected virtual void UU2()
    {
        //this.gameObject.SetActive(false);
        bt = !bt;
        transform.position = Way2.position;
        //Way2 = RaceCircle2.GetNextWaypoint(Way2);
        UU1();
    }

    protected override void UU1()
    {
        base.UU1();
    }
}
