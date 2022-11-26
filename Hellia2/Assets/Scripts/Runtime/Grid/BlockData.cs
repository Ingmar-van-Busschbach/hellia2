using System;

namespace Runtime.Grid
{
    [Serializable]
    public struct BlockData
    {
        public BlockType type;
        public Direction direction;
    }
}