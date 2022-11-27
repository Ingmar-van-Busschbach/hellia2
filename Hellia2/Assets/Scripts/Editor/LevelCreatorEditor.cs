using System;
using NUnit.Framework.Internal;
using Runtime;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LevelCreator))]
    public class LevelCreatorEditor : UnityEditor.Editor
    {
        private float _currentY = 0;
        
        private static readonly Color BorderColor = new Color(0, 0, 0);
        private static readonly Color HoverColor = new Color(0, 255, 0);
        private static readonly Color DefaultColor = new Color(25, 25, 25);

        private void OnSceneGUI()
        {
            int controlId = GUIUtility.GetControlID(FocusType.Passive);
            
            switch (Event.current.type)
            {
                case EventType.ScrollWheel:
                    if (!Event.current.shift) break;

                    _currentY += Event.current.delta.y > 0 ? -1 : 1;
                    
                    // Tell the UI your event is the main one to use, it override the selection in  the scene view
                    GUIUtility.hotControl = controlId;
                    // Don't forget to use the event
                    Event.current.Use();
                    break;
            }
            DrawCubes();
            DrawMath();
        }
            
        private void DrawCubes() {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Vector3[] verts = new Vector3[]
                    {
                        new(i, _currentY, j),
                        new(i + 1, _currentY, j),
                        new(i + 1, _currentY, j + 1),
                        new(i, _currentY, j + 1)
                    };

                    Handles.DrawSolidRectangleWithOutline(verts, DefaultColor, BorderColor);
                }
            }
        }

        private void DrawMath()
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            
            float angle = Vector3.Angle(Vector3.down, ray.direction);
            
            float height = ray.origin.y - _currentY;
            float distance = height / Mathf.Sin(angle);
            
            // Debug.Log($"{angle} : {height}");
            
            
            Handles.DrawWireCube(ray.origin + ray.direction * distance, Vector3.one);
            
            Debug.LogWarning(ray.origin + ray.direction * distance);
        }
        
    }
}
