using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePath : MonoBehaviour
{
    [Header("Path")]
    public PathNode[] Path;
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

    }


    [System.Serializable]
    public struct PathNode
    {
        public Vector3 Position;
        public Vector3 AlternativePosition;
        public Vector3 FallbackPosition;
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

}
