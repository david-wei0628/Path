using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Point))]
public class PointEditor : Editor
{
    //private Mode mode;

    private void OnSceneGUI()
    {
        Point point = (Point)this.target;
        if(point.PR == null)
        {
            return;
        }
        for(int i = 0; i < point.PR.Length; i++)
        {
            Point.PointR node = point.PR[i];
            if(this.Handle(ref node.position))
            {
                Undo.RecordObject(point, "Waypoint");
                point.PR[i] = node;
                EditorUtility.SetDirty(point);
            }
         }
    }

    private bool Handle(ref Vector3 position)
    {
        Vector3 newPosition = Handles.PositionHandle(position, Quaternion.identity);
        bool changed = newPosition != position;
        position = newPosition;
        return changed;
    }
}
