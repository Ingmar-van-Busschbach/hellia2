using Runtime.Grid;
using UnityEngine;

namespace Runtime.Blocks
{
    public class ImmovableBlock : BaseBlock
    {
        public override BlockType BlockType => BlockType.Immovable;
        protected override bool CanMoveTo(Vector3Int newPosition)
        {
            return false;
        }

        public override bool CanBeTakenOverBy(BaseBlock baseBlock, Vector3Int direction)
        {
            return false;
        }

        protected override bool TakeOver(BaseBlock baseBlock, Vector3Int direction)
        {
            return false;
        }
    }
}
