using System;
using UnityEngine;

namespace Runtime
{
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private float currentY;
        [SerializeField] private float distance = 15;
        
        [Header("Debug values, don't change")]
        [SerializeField] private float height;
        [SerializeField] private float calculatedDistance;
        [SerializeField] private float angle;
        [SerializeField] private float hitHeight;

        private Ray? _ray = null;
        
        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayHitPoint = ray.origin + ray.direction * distance;
            
            angle = Vector3.Angle(Vector3.down, ray.direction);
            
            height = rayHitPoint.y - ray.origin.y;
            calculatedDistance = height / Mathf.Sin(angle);
            
            hitHeight = rayHitPoint.y;
            
            Debug.DrawLine(ray.origin, rayHitPoint, Color.green);
            Debug.DrawLine(rayHitPoint, new Vector3(ray.origin.x, (rayHitPoint).y, ray.origin.z), Color.blue);
            Debug.DrawLine(ray.origin, new Vector3(ray.origin.x, (rayHitPoint).y, ray.origin.z), Color.red);
            
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * calculatedDistance, Color.yellow);
        }

        private void OnDrawGizmos()
        {
            if (_ray == null) return;
            Gizmos.DrawSphere(_ray.Value.origin, 1);
            Gizmos.DrawSphere(_ray.Value.origin + _ray.Value.direction * distance, 1);
        }
    }
}
