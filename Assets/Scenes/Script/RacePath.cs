using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePath : MonoBehaviour
{
    [Header("Path")]
    public PathNode[] Path;
    public PathHelper[] Helpers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
		if (this.Path == null)
		{
			return;
		}

		Color orange = new Color(1f, 0.5f, 0.2f);
		for (int waypointIndex = 0; waypointIndex < this.Path.Length; waypointIndex++)
		{
			int nextWaypointIndex = waypointIndex < this.Path.Length - 1 ? waypointIndex + 1 : 0;

			PathNode node = this.Path[waypointIndex];
			PathNode nextNode = this.Path[nextWaypointIndex];

			Debug.DrawLine(node.Position, nextNode.Position, Color.red);

			if (node.HasAlternative || nextNode.HasAlternative)
			{
				Debug.DrawLine(node.AlternativePosition, nextNode.AlternativePosition, Color.blue);
			}

			if (node.HasFallback || nextNode.HasFallback)
			{
				Debug.DrawLine(node.FallbackPosition, nextNode.FallbackPosition, Color.black);
			}

#if UNITY_EDITOR
			UnityEditor.Handles.Label(node.Position, waypointIndex.ToString());
#endif
		}

//#if UNITY_EDITOR
//		for (int i = 0; i < this.Helpers.Length; i++)
//		{
//			RacePath.PathHelper helper = this.Helpers[i];

//			UnityEditor.Handles.DrawWireDisc(helper.Start, Vector3.up, helper.Radius);
//			UnityEditor.Handles.DrawWireDisc(helper.Goal, Vector3.up, helper.Radius * 0.5f);

//			Vector3 perpendicular = Vector3.Normalize(Vector3.Cross(helper.Goal - helper.Start, Vector3.up));
//			Debug.DrawLine(helper.Start + perpendicular * helper.Radius, helper.Goal + perpendicular * helper.Radius * 0.5f);
//			Debug.DrawLine(helper.Start - perpendicular * helper.Radius, helper.Goal - perpendicular * helper.Radius * 0.5f);
//		}
//#endif
	}

	public Vector3 GetPosition(float ratioOnPath, bool alternative)
	{
		int firstIndex = Mathf.FloorToInt(ratioOnPath % this.Path.Length);
		int secondIndex = Mathf.FloorToInt((ratioOnPath + 1) % this.Path.Length);
		float ratio = ratioOnPath % 1f;

		return Vector3.Lerp(this.Path[firstIndex].GetPosition(alternative), this.Path[secondIndex].GetPosition(alternative), ratio);
	}

	public Vector3 GetPerpendicular(float ratioOnPath, bool alternative)
	{
		Vector3 forward = default;
		int waypointIndex = (int)ratioOnPath;

		if (waypointIndex >= 1)
		{
			forward += Vector3.Normalize(this.Path[waypointIndex].GetPosition(alternative) - this.Path[waypointIndex - 1].GetPosition(alternative));
		}

		if (waypointIndex < this.Path.Length - 1)
		{
			forward += Vector3.Normalize(this.Path[waypointIndex + 1].GetPosition(alternative) - this.Path[waypointIndex].GetPosition(alternative));
		}

		return Vector3.Cross(forward.normalized, Vector3.up);
	}

	[System.Serializable]
	public struct PathNode
	{
		public Vector3 Position;
		public Vector3 AlternativePosition;
		public Vector3 FallbackPosition;
		public float RecommendedSpeedMultiplier;
		public float Drift;

		public bool HasAlternative
		{
			get
			{
				return this.Position != this.AlternativePosition;
			}
		}

		public bool HasFallback
		{
			get
			{
				return this.Position != this.FallbackPosition;
			}
		}

		public Vector3 GetPosition(bool alternative)
		{
			if (alternative)
			{
				return this.AlternativePosition;
			}
			else
			{
				return this.Position;
			}
		}

		public void Uniformize()
		{
			this.AlternativePosition = this.Position;
			this.FallbackPosition = this.Position;
		}
	}

	[System.Serializable]
	public struct PathHelper
	{
		public Vector3 Start;
		public float Radius;

		public Vector3 Goal;
	}
}
