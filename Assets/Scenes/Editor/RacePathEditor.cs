using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RacePath))]
public class RacePathEditor : Editor
{
    private void OnSceneGUI()
    {
        RacePath RacePoint = (RacePath)this.target;
        if (RacePoint.Path == null)
        {
            return;
        }
        for (int i = 0; i < RacePoint.Path.Length; i++)
        {
            RacePath.PathNode node = RacePoint.Path[i];
            if (this.Handle(ref node.Position))
            {
                Undo.RecordObject(RacePoint, "Waypoint");
                RacePoint.Path[i] = node;
                EditorUtility.SetDirty(RacePoint);
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
