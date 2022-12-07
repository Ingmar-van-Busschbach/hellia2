using Runtime.Blocks;
using Runtime.Blocks.LightBlocks;
using UnityEngine;
using Utilities;

namespace Runtime.Data
{
    public struct LightEmitData
    {
        public Vector3Int Direction;
        public BaseLightBlock Emitter;
        public BaseBlock HitBlock;
        public Vector3Int HitPosition;

        public bool HitLightBlock => HitBlock != null && HitBlock.GetType().IsSubclassOf(typeof(BaseLightBlock));
        
        public LightEmitData(Vector3Int direction, BaseLightBlock emitter, Vector3Int defaultHitPos)
        {
            Direction = direction;
            Emitter = emitter;
            HitBlock = null;
            HitPosition = defaultHitPos;
        }
    }
}
