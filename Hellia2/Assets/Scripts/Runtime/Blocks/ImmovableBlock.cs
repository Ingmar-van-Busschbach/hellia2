using Runtime.Grid;
using UnityEngine;

namespace Runtime.Blocks
{
    public class ImmovableBlock : BaseBlock
    {
        public override BlockType BlockType => BlockType.Immovable;
        public override bool TryOverTake(Vector3Int newPosition)
        {
            return false;
        }

        public override bool CanBeTakenOverBy(BaseBlock baseBlock, Vector3Int direction)
        {
            return false;
        }

        public override bool OnGettingTakenOver(BaseBlock baseBlock, Vector3Int direction)
        {
            return false;
        }
    }
}
