using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(World))]
public class WorldEditor : Editor
{
    private void OnSceneGUI()
    {
        World world = (World)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(world.transform.position, Vector3.up, Vector3.forward, 360, world.maxDistance);
    }
}
