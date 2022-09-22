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

     private void OnDrawGizmos()
    {
        if (this.PR == null)
        {
            return;
        }

        Color orange = new Color(1f, 0.5f, 0.2f);
        for (int waypointIndex = 0; waypointIndex < this.PR.Length; waypointIndex++)
        {
            int nextWaypointIndex = waypointIndex < this.PR.Length - 1 ? waypointIndex + 1 : 0;

            PointR node = this.PR[waypointIndex];
            PointR nextNode = this.PR[nextWaypointIndex];

            Debug.DrawLine(node.position, nextNode.position, Color.red);
            

#if UNITY_EDITOR
            UnityEditor.Handles.Label(node.position, waypointIndex.ToString());
#endif
        }

    }

    [Serializable]
    public struct PointR
    {
        public Vector3 position;
    }
}
