using System;
using Runtime.Grid;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks
{
    public class ClimbableBlock : BaseBlock
    {
        [SerializeField] private Directions allowedDirections;
        
        public override BlockType BlockType => BlockType.Climbable;

        public override bool TryOverTake(Vector3Int newPosition)
        {
            return false;
        }

        public override bool CanBeTakenOverBy(BaseBlock baseBlock, Vector3Int direction)
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

        public override bool OnGettingTakenOver(BaseBlock baseBlock, Vector3Int direction)
        {
            return false;
        }

        public Directions AllowedDirections => allowedDirections;
    }
}
