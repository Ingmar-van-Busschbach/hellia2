using System;
using Runtime.Grid;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks
{
    public class MoveableBlock : BaseBlock
    {
        public override BlockType BlockType => BlockType.Moveable;
        protected override bool CanMoveTo(Vector3Int newPosition)
        {
            Vector3Int myPos = transform.position.ToVector3Int();
            Vector3Int direction = newPosition - myPos;
        
            BaseBlock blockAtLocation = GridManager.Instance.GetBlockAt(newPosition);
            BaseBlock floorBlockAtLocation = GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down);
            
            if (blockAtLocation != null) return false;
            if (floorBlockAtLocation == null && !CanFallAt(myPos + direction)) return false;
            return true;
        }

        public override bool CanBeTakenOverBy(BaseBlock baseBlock, Vector3Int direction)
        {
            BaseBlock nextBlock = GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + direction);
            
            // if we can't move to the direction we are being pushed. we cannot be overtaken
            if (!CanMoveTo(transform.position.ToVector3Int() + direction))
            {
                return false;
            }
            
            switch (baseBlock.BlockType)
            {
                case BlockType.Moveable:
                case BlockType.Immovable:
                case BlockType.Breakable:
                case BlockType.Meltable:
                case BlockType.Floor:
                case BlockType.Wall:
                    return false;
                case BlockType.Player:
                    if (nextBlock != null) return false;
                    return true;
                default:
                    return false;
            }
        }

        protected override bool TakeOver(BaseBlock baseBlock, Vector3Int direction)
        {
            switch (baseBlock.BlockType)
            {
                case BlockType.Moveable:
                case BlockType.Immovable:
                case BlockType.Breakable:
                case BlockType.Meltable:
                case BlockType.Floor:
                case BlockType.Wall:
                    return false;
                case BlockType.Player:
                    transform.position += direction;
                    if (CanFallAt(transform.position.ToVector3Int())) DoFall();
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns whether or not this object should fall down. 
        /// </summary>
        /// <returns></returns>
        protected bool CanFallAt(Vector3Int location)
        {
            Vector3Int myPosition = location;
            BaseBlock firstFloorBlock = GridManager.Instance.GetBlockAt(myPosition + (Vector3Int.down));
            if (firstFloorBlock != null)
            {
                return false;
            }
            
            for (int i = 1; i < 10; i++)
            {
                BaseBlock block = GridManager.Instance.GetBlockAt(myPosition + (Vector3Int.down * i));
                if (block != null)
                {
                    return true;
                }
            }
            
            return false;
        }

        protected void DoFall()
        {
            Vector3Int myPosition = transform.position.ToVector3Int();
            for (int i = 1; i < 10; i++)
            {
                BaseBlock block = GridManager.Instance.GetBlockAt(myPosition + (Vector3Int.down * i));
                
                if (block == null) continue;
                transform.position = block.transform.position.ToVector3Int() + Vector3Int.up;
                break;
            }
        }
    }
}