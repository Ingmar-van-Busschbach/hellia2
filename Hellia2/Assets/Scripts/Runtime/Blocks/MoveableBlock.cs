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
            Vector3Int direction =newPosition - myPos;
        
            BaseBlock blockAtLocation = GridManager.Instance.GetBlockAt(newPosition);
            BaseBlock floorBlockAtLocation = GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down);

            if (blockAtLocation != null) return false;
            if (floorBlockAtLocation == null) return false;
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
                case BlockType.Hole:
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
                case BlockType.Hole:
                    return false;
                case BlockType.Player:
                    transform.position += direction;
                    return true;
                default:
                    return false;
            }
        }
    }
}