using System;
using Runtime;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using Utilities;


[CustomEditor(typeof(LevelCreator))]
public class LevelCreatorEditor : Editor
{
    private RaycastHit? _raycastHit = null;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelCreator levelCreator = target as LevelCreator;
        if (levelCreator == null) return;

        if (levelCreator.transform.childCount == 0)
        {
            if (!GUILayout.Button("Create new level")) return;

            levelCreator.CreateNewMapData();
            levelCreator.SpawnDefaultFloor();
        }
        else
        {
            if (GUILayout.Button("Save current level"))
            {
                levelCreator.SaveLevel();
            }

            if (GUILayout.Button("Destroy current level"))
            {
                levelCreator.DestroyCurrentMap();
            }
        }
    }

    private void OnSceneGUI()
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        if (Physics.Raycast(ray, out var hitData))
        {
            _raycastHit = hitData;
            Vector3 hitObjectPosition = hitData.collider.gameObject.transform.position;

            Handles.color = Color.yellow;
            Handles.DrawWireCube(hitObjectPosition, Vector3.one);
            if (Event.current.type == EventType.MouseMove)
            {
                SceneView.RepaintAll();
            }

            DrawSelectingFace(hitData, hitObjectPosition);
            HandleInput();
        }
        else
        {
            _raycastHit = null;
        }
    }

    private void DrawSelectingFace(RaycastHit hitData, Vector3 hitObjectPosition)
    {
        Vector3[] verts = new Vector3[4];
        if (hitData.normal == Vector3.up || hitData.normal == Vector3.down)
        {
            float offsetY = hitData.normal == Vector3.up ? .5f : -.5f;
            verts = new[]
            {
                new Vector3(hitObjectPosition.x - .5f, hitObjectPosition.y + offsetY, hitObjectPosition.z - .5f),
                new Vector3(hitObjectPosition.x - .5f, hitObjectPosition.y + offsetY, hitObjectPosition.z + .5f),
                new Vector3(hitObjectPosition.x + .5f, hitObjectPosition.y + offsetY, hitObjectPosition.z + .5f),
                new Vector3(hitObjectPosition.x + .5f, hitObjectPosition.y + offsetY, hitObjectPosition.z - .5f)
            };
        }
        else if (hitData.normal == Vector3.right || hitData.normal == Vector3.left)
        {
            float offsetX = hitData.normal == Vector3.right ? .5f : -.5f;
            verts = new[]
            {
                new Vector3(hitObjectPosition.x + offsetX, hitObjectPosition.y + .5f, hitObjectPosition.z - .5f),
                new Vector3(hitObjectPosition.x + offsetX, hitObjectPosition.y + .5f, hitObjectPosition.z + .5f),
                new Vector3(hitObjectPosition.x + offsetX, hitObjectPosition.y - .5f, hitObjectPosition.z + .5f),
                new Vector3(hitObjectPosition.x + offsetX, hitObjectPosition.y - .5f, hitObjectPosition.z - .5f)
            };
        }
        else if (hitData.normal == Vector3.forward || hitData.normal == Vector3.back)
        {
            float offsetZ = hitData.normal == Vector3.forward ? .5f : -.5f;
            verts = new[]
            {
                new Vector3(hitObjectPosition.x + .5f, hitObjectPosition.y + .5f, hitObjectPosition.z + offsetZ),
                new Vector3(hitObjectPosition.x - .5f, hitObjectPosition.y + .5f, hitObjectPosition.z + offsetZ),
                new Vector3(hitObjectPosition.x - .5f, hitObjectPosition.y - .5f, hitObjectPosition.z + offsetZ),
                new Vector3(hitObjectPosition.x + .5f, hitObjectPosition.y - .5f, hitObjectPosition.z + offsetZ)
            };
        }

        Handles.color = Color.green;
        Handles.DrawSolidRectangleWithOutline(verts, new Color(0.5f, 0.5f, 0.5f, 0.1f), new Color(0, 0, 0, 1));
    }

    private void HandleInput()
    {
        int controlId = GUIUtility.GetControlID(FocusType.Passive);

        switch (Event.current.type)
        {
            case EventType.MouseDown:
                Vector3Int targetLocation = (_raycastHit.Value.collider.gameObject.transform.position + _raycastHit.Value.normal).ToVector3Int();
                LevelCreator levelCreator = target as LevelCreator;
                if (levelCreator == null) return;

                if (Event.current.button != 0) return;

                if (!Event.current.control)
                {
                    levelCreator.PlaceFloorBlock(targetLocation);
                }
                else
                {
                    levelCreator.DestroyBlock(_raycastHit.Value.collider.gameObject.transform.position.ToVector3Int());
                }

                // Tell the UI your event is the main one to use, it override the selection in  the scene view
                GUIUtility.hotControl = controlId;
                // Don't forget to use the event
                Event.current.Use();
                break;
        }
    }
}