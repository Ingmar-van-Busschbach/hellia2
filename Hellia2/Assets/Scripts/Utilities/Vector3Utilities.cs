using UnityEngine;

namespace Utilities
{
    public static class Vector3Utilities
    {
        public static Vector3Int ToVector3Int(this Vector3 vector)
        {
            return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
        }

        public static Directions ToDirectionsFlag(this Vector3Int vector)
        {
            Directions directions = Directions.Nothing;
            if (vector.z > 0) directions |= Directions.Forward;
            if (vector.z < 0) directions |= Directions.Backward;
            if (vector.x > 0) directions |= Directions.Right;
            if (vector.x < 0) directions |= Directions.Left;
            if (vector.y > 0) directions |= Directions.Up;
            if (vector.y < 0) directions |= Directions.Down;
            return directions;
        }
    }
}
