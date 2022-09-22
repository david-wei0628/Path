using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U1 : MonoBehaviour
{
    [SerializeField] public RaceCircle RaceCircle;
    [SerializeField] public RacePath RacePath;
    //public RaceCircle RaceCircle;   
    // Start is called before the first frame update
    protected virtual void Start()
    {

        Debug.Log("UUI");
    }
    protected bool U1Bool;
    // Update is called once per frame
    void Update()
    {
        
    }

    

    protected virtual void UU1()
    {
        //this.gameObject.transform.position = Vector3.zero;
    }
}
