using Runtime.Blocks.LightBlocks;
using UnityEngine;

namespace Runtime.Data
{
    public struct LightLoseData
    {
        public BaseLightBlock Emitter;
        public Vector3Int Direction;

        public LightLoseData(Vector3Int direction, BaseLightBlock emitter)
        {
            Direction = direction;
            Emitter = emitter;
        }
    }
}
