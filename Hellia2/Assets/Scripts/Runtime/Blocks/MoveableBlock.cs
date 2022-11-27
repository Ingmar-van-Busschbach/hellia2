using System;
using Runtime.Grid;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks
{
    public class MoveableBlock : BaseBlock
    {
        public override bool CanMove(Vector3Int direction)
        {
            Vector3Int targetPos = transform.position.ToVector3Int() + direction;
            BlockData targetBlockData = GridManager.Instance.MapData.GetBlockDataAt(targetPos);
            
            if (targetBlockData.type == BlockType.Empty)
            {
                return true;
            }

            return false;
        }

        public override bool TryMove(Vector3Int direction)
        {
            if (!CanMove(direction)) return false;
            transform.position += direction;
            return true;
        }

        public override bool CanBeOvertakenByType(BlockType type)
        {
            switch (type)
            {
                case BlockType.Moveable:
                case BlockType.Immovable:
                case BlockType.Breakable:
                case BlockType.Meltable:
                case BlockType.Floor:
                case BlockType.Wall:
                case BlockType.Hole:
                case BlockType.Empty:
                    return false;
                case BlockType.Player:
                    return true;
                default:
                    return false;
            }
        }

        public override void HandleOvertakingBy(BlockData blockData, Vector3Int direction)
        {
            switch (blockData.type)
            {
                case BlockType.Player:
                    transform.position += direction;
                    break;
                default:
                    return;
            }
        }
    }
}