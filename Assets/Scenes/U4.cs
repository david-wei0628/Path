using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class U4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public static U4 U4U;
    private void Awake()
    {
        U4.U4U = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UU3()
    {
        Debug.Log("123");
    }
}
