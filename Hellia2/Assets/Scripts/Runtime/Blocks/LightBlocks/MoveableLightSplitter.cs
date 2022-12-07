using Runtime.Blocks.Attributes;
using Runtime.Grid;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks.LightBlocks
{
    public class MoveableLightSplitter : LightSplitter
    {
        [CanInteract]
        public bool CanBePushedByPlayer(PlayerBlock playerBlock, Vector3Int direction)
        {
            Vector3Int newPosition = transform.position.ToVector3Int() + direction;
            BaseBlock blockAtLocation = GridManager.Instance.GetBlockAt(newPosition);
            BaseBlock floorBlockAtLocation = GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down);
            
            if (blockAtLocation != null) return false;
            if (floorBlockAtLocation == null && !CanFallAt(newPosition)) return false;
            return true;
        }

        [DoInteract]
        public void OnInteractedByPlayer(PlayerBlock playerBlock, Vector3Int direction)
        {
            GridManager.Instance.Move(this, transform.position.ToVector3Int() + direction);
        }
        
        /// <summary>
        /// Returns whether or not this object should fall down. 
        /// </summary>
        /// <returns></returns>
        protected bool CanFallAt(Vector3Int location)
        {
            GridManager gridManager = GridManager.Instance;
         
            Vector3Int myPosition = location;
            BaseBlock firstFloorBlock = gridManager.GetBlockAt(myPosition + (Vector3Int.down));
            if (firstFloorBlock != null)
            {
                return false;
            }
            var baseBlock = gridManager.GetFirstBlockInDirection(myPosition + Vector3Int.down,  Vector3Int.down, 10);
            return baseBlock != null;
        }
    }
}
