using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U3 : U2
{
    //[SerializeField] private RaceCircle RaceCircle;

    private Transform Way;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Debug.Log("UUI3");
        Way = RaceCircle.GetNextWaypoint(Way);
        transform.position = Way.position;
        Way = RaceCircle.GetNextWaypoint(Way);
        transform.LookAt(Way);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UU2();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            U4.U4U.UU3();
        }
        transform.position = Vector3.MoveTowards(transform.position, Way.position, Time.deltaTime * 10f);
        if (Vector3.Distance(transform.position, Way.position) < 0.1f)
        {
            Way = RaceCircle.GetNextWaypoint(Way);
        }
    }

    protected override void UU2()
    {
        base.UU2();
    }

    private void U31()
    {
        //this.animator2.Play();
    }
}
