using Runtime.Grid;
using UnityEngine;

namespace Runtime.Blocks
{
    /// <summary>
    /// The base block is used for the floor or immovable objects and simply does not handle any interaction with it.
    /// </summary>
    public abstract class BaseBlock : MonoBehaviour
    {
        public abstract bool CanMove(Vector3Int direction);
        public abstract bool TryMove(Vector3Int direction);
        public abstract bool CanBeOvertakenByType(BlockType type);
        public abstract void HandleOvertakingBy(BlockData blockData, Vector3Int direction);
    }
}