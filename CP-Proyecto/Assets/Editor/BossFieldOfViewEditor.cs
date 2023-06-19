using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(BossFieldOfView))]
public class BossFieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        BossFieldOfView fov = (BossFieldOfView)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);
        
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle/2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle/2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

        Handles.color = Color.blue;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.awarenessRadius);
    }
}
