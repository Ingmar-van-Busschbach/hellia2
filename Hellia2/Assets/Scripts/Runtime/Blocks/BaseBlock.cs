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

        protected void TryMove(Vector3Int newPosition, bool moveIfOvertakenBlock = true)
        {
            Vector3Int myPos = transform.position.ToVector3Int();
            Vector3Int direction =newPosition - myPos;
            bool overtakenBlock = false;
            
            BaseBlock block = GridManager.Instance.GetBlockAt(newPosition);
            if (block != null)
            {
                if (!block.CanBeTakenOverBy(this, direction)) return;
                block.TakeOver(this, direction);
                overtakenBlock = true;
            }

            if (!moveIfOvertakenBlock && overtakenBlock) return;
            transform.position = newPosition;
        }
        
        /// <summary>
        /// Whether or not this object can move to this location. Can be used to check for specific flooring etc,
        /// </summary>
        /// <param name="newPosition">The new position the object is moving to</param>
        /// <returns>Whether or not you can move to this position</returns>
        protected abstract bool CanMoveTo(Vector3Int newPosition);

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
        protected abstract bool TakeOver(BaseBlock baseBlock, Vector3Int direction);
    }
}