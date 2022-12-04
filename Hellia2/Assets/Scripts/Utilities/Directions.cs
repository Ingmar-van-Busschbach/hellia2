using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// The direction flag is a flag which direction something is going.
    /// the id of your flag should be power of 2
    /// </summary>

    [System.Flags]
    public enum Directions
    {
        Nothing = 0, // nothing
        Up = 1,  // y++
        Down = 2,  // y--
        Forward = 4, //z++
        Backward = 8, //z--
        Left = 16, //x--
        Right = 32, //x++
    }

    public static class DirectionsExtensions
    {
        public static Vector3Int ToVector3Int(this Directions directions)
        {
            if (directions.HasFlag(Directions.Up)) return Vector3Int.up;
            if (directions.HasFlag(Directions.Down)) return Vector3Int.down;
            if (directions.HasFlag(Directions.Left)) return Vector3Int.left;
            if (directions.HasFlag(Directions.Right)) return Vector3Int.right;
            if (directions.HasFlag(Directions.Forward)) return Vector3Int.forward;
            if (directions.HasFlag(Directions.Backward)) return Vector3Int.back;
            return Vector3Int.zero;
        }
        
        public static Directions Flip(this Directions directions)
        {
            if (directions.HasFlag(Directions.Up)) return Directions.Down;
            if (directions.HasFlag(Directions.Down)) return Directions.Up;
            
            if (directions.HasFlag(Directions.Left)) return Directions.Right;
            if (directions.HasFlag(Directions.Right)) return Directions.Left;
            
            if (directions.HasFlag(Directions.Backward)) return Directions.Forward;
            if (directions.HasFlag(Directions.Forward)) return Directions.Backward;

            return Directions.Nothing;
        }
    }
}
