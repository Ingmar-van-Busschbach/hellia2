using System;
using Runtime.Grid;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks
{
    /// <summary>
    /// The base block is used for the floor or immovable objects and simply does not handle any interaction with it.
    /// </summary>
    public abstract class BaseBlock : MonoBehaviour
    {
        public abstract BlockType BlockType
        {
            get;
        }

        public BaseBlock GetBlockBeneath()
        {
            return GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + Vector3Int.down);
        }
        
        public BaseBlock GetBlockAbove()
        {
            return GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + Vector3Int.up);
        }
    }
}