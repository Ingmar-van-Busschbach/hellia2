using UnityEngine;

namespace Runtime.Blocks.BlockInterfaces
{
    public interface IPushable
    {
        public bool CanPush(BaseBlock baseBlock, Vector3Int direction);
        public void Push(BaseBlock baseBlock, Vector3Int direction);
    };
}
