using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCircle : MonoBehaviour
{
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
		foreach(Transform t in transform)
        {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(t.position, 1f);
        }

		Gizmos.color = Color.black;
		for(int i=0;i<transform.childCount-1;i++)
        {
			Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
        }
		Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
	}

	public Transform GetNextWaypoint(Transform Waypoint)
    {
		if(Waypoint == null)
        {
			return transform.GetChild(0);
        }

		if(Waypoint.GetSiblingIndex() < transform.childCount-1)
        {
			return transform.GetChild(Waypoint.GetSiblingIndex() + 1);
        }
        else
        {
			return transform.GetChild(0);
        }
    }

}
