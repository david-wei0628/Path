using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U2 : U1
{
    [SerializeField] private RaceCircle RaceCircle;
    private Transform Way;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Debug.Log("UUI2"); 
        
    }
    private void Awake()
    {
        Way = RaceCircle.GetNextWaypoint(Way);
        transform.position = Way.position;
        Way = RaceCircle.GetNextWaypoint(Way);
        transform.LookAt(Way);
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
            transform.position = Vector3.MoveTowards(transform.position, Way.position, Time.deltaTime * 10f);
            if (Vector3.Distance(transform.position, Way.position) < 0.1f)
            {
                Way = RaceCircle.GetNextWaypoint(Way);
            }
        }
        
    }
    protected virtual void UU2()
    {
        //this.gameObject.SetActive(false);
        bt = !bt;
        UU1();
    }

    protected override void UU1()
    {
        base.UU1();
    }
}
