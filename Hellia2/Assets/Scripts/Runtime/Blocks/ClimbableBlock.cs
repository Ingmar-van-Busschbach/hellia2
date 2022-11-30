using System;
using Runtime.Grid;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks
{
    public class ClimbableBlock : BaseBlock, IClimbable
    {
        [SerializeField] private Directions allowedDirections;
        
        public override BlockType BlockType => BlockType.Climbable;

        public Directions AllowedDirections => allowedDirections;
        public bool CanClimbUp(BaseBlock baseBlock, Vector3Int direction)
        {
            switch (baseBlock.BlockType)
            {
                case BlockType.Climbable:
                case BlockType.Moveable:
                case BlockType.Immovable:
                case BlockType.Breakable:
                case BlockType.Meltable:
                case BlockType.Floor:
                case BlockType.Wall:
                    return false;
                case BlockType.Player:
                    // The climbable needs to support this direction before we can climb it.
                    return allowedDirections.HasFlag(direction.ToDirectionsFlag());
                default:
                    return false;
            }
        }

        public bool CanClimbDown(BaseBlock baseBlock, Vector3Int direction)
        {
            Vector3Int reversedDirection = new Vector3Int(-direction.x, -direction.y, -direction.z);
            switch (baseBlock.BlockType)
            {
                case BlockType.Climbable:
                case BlockType.Moveable:
                case BlockType.Immovable:
                case BlockType.Breakable:
                case BlockType.Meltable:
                case BlockType.Floor:
                case BlockType.Wall:
                    return false;
                case BlockType.Player:
                    // The climbable needs to support this direction before we can climb it.
                    return allowedDirections.HasFlag(reversedDirection.ToDirectionsFlag());
                default:
                    return false;
            }
        }
    }
}
