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

        /// <summary>
        /// Try overtake the block at the specific position. 
        /// </summary>
        /// <param name="newPosition">The position we try to overtake</param>
        public abstract bool TryOverTake(Vector3Int newPosition);

        /// <summary>
        /// Called when a block tries to overtake you. 
        /// </summary>
        /// <param name="baseBlock">The block trying to overtake you</param>
        /// <param name="direction">The block's moving direction</param>
        public abstract bool CanBeTakenOverBy(BaseBlock baseBlock, Vector3Int direction);

        /// <summary>
        /// When a block tries to overtake you, this function is called
        /// </summary>
        /// <param name="baseBlock">The block overtaking you</param>
        /// <param name="direction">The direction the block is moving</param>
        /// <returns></returns>
        public abstract bool OnGettingTakenOver(BaseBlock baseBlock, Vector3Int direction);

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