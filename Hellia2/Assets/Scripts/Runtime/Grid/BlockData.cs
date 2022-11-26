using System;
using SimpleJSON;

namespace Runtime.Grid
{
    [System.Serializable]
    public struct BlockData
    {
        public BlockType type;
        public Direction direction;

        public BlockData(JSONNode jsonNode)
        {
            type = jsonNode.HasKey("type")
                ? (BlockType) Enum.Parse(typeof(BlockType), jsonNode["type"].Value)
                : BlockType.Empty;
            
            direction = jsonNode.HasKey("direction")
                ? (Direction) Enum.Parse(typeof(Direction), jsonNode["direction"].Value)
                : Direction.None;
        }
    }
}