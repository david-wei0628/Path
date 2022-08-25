using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Point : MonoBehaviour
{
    public PointR[] PR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    public struct PointR
    {
        public Vector3 position;
        public Vector3 forward;
        public Vector3 noemal;
        GameObject go;

        
        
        
    }
}
