namespace Utilities
{
    /// <summary>
    /// The direction flag is a flag which direction something is going.
    /// the id of your flag should be power of 2
    /// </summary>

    [System.Flags]
    public enum Directions
    {
        Nothing = 0,
        Up = 1, // y+
        Down = 2, // y-
        Forward = 4, //x+
        Backward = 8, //x-
        Left = 16, //z-
        Right = 32, //z+
    }
}
