using UnityEngine;

namespace Utilities
{
    public static class Vector3Utilities
    {
        public static Vector3Int ToVector3Int(this Vector3 vector)
        {
            return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
        }
    }
}
