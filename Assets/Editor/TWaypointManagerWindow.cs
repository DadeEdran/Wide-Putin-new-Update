using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TWaypointManagerWindow:EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<TWaypointManagerWindow>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root trasform must be selected. pLease assign a root trasform.",MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    void DrawButtons()
    {
        if (GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }
        if(Selection.activeGameObject !=null && Selection.activeGameObject.GetComponent<TWaypoint>())
        {
            if (GUILayout.Button("Add Branch Waypoint"))
            {
                CreateBranch();
            }
            if(GUILayout.Button("Create Waypoint Before"))
            {
                CreateWaypointBefore();
            }
            if(GUILayout.Button("Create Waypoint After"))
            {
                CreateWaypointAfter();
            }
            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint();
            }
        }
    }

    void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint "+waypointRoot.childCount,typeof(TWaypoint));
        waypointObject.transform.SetParent(waypointRoot,false);

        TWaypoint waypoint = waypointObject.GetComponent<TWaypoint>();

        if (waypointRoot.childCount > 1)
        {
            waypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount-2).GetComponent<TWaypoint>();
            waypoint.previousWaypoint.NextWaypoint = waypoint;
            // place the waypoint at the last
            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
        }

        Selection.activeGameObject = waypoint.gameObject;
    }


    void CreateWaypointBefore()
    {
        GameObject waypointObject = new GameObject("Waypoint"+waypointRoot.childCount,typeof(TWaypoint));
        waypointObject.transform.SetParent(waypointRoot,false);

        TWaypoint newWaypoint = waypointObject.GetComponent<TWaypoint>();

        TWaypoint selectedWaypoint=Selection.activeGameObject.GetComponent<TWaypoint>();
        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        if(selectedWaypoint.previousWaypoint != null)
        {
            newWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
            selectedWaypoint.previousWaypoint.NextWaypoint = newWaypoint;
        }

        newWaypoint.NextWaypoint = selectedWaypoint;
        selectedWaypoint.previousWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void CreateWaypointAfter()
    {
        GameObject waypointObject = new GameObject("Waypoint" + waypointRoot.childCount, typeof(TWaypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        TWaypoint newWaypoint = waypointObject.GetComponent<TWaypoint>();

        TWaypoint selectedWaypoint = Selection.activeGameObject.GetComponent<TWaypoint>();
        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        newWaypoint.previousWaypoint = selectedWaypoint;

        if (selectedWaypoint.NextWaypoint != null)
        {
            selectedWaypoint.NextWaypoint.previousWaypoint = newWaypoint;
            newWaypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
        }

        selectedWaypoint.NextWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void RemoveWaypoint()
    {
        TWaypoint selectedWaypoint = Selection.activeGameObject.GetComponent<TWaypoint>();

        if (selectedWaypoint.NextWaypoint!=null)
        {
            selectedWaypoint.NextWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
        }
        if (selectedWaypoint.previousWaypoint != null)
        {
            selectedWaypoint.previousWaypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
            Selection.activeGameObject = selectedWaypoint.previousWaypoint.gameObject;
        }
        DestroyImmediate(selectedWaypoint.gameObject);
    }

    void CreateBranch()
    {
        GameObject waypointObject = new GameObject("Waypoint "+waypointRoot.childCount,typeof(TWaypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        TWaypoint waypoint = waypointObject.GetComponent<TWaypoint>();

        TWaypoint branchedFrom = Selection.activeGameObject.GetComponent<TWaypoint>();

        branchedFrom.branches.Add(waypoint);

        waypoint.transform.position = branchedFrom.transform.position;

        waypoint.transform.forward = branchedFrom.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;
    }
}
