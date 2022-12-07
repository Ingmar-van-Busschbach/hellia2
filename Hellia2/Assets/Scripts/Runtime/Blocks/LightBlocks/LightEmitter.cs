using Runtime.Data;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks.LightBlocks
{
    public class LightEmitter : BaseLightBlock
    {
        protected override LightEmitData EmitLight(Vector3Int direction)
        {
            LightEmitData data = base.EmitLight(direction);
            RenderLight(data);
            return data;
        }

        public void RenderLight(LightEmitData data)
        {
            Debug.DrawLine(transform.position.ToVector3Int(), data.HitPosition, Color.green);
        }
        
        protected override void Update()
        {
            base.Update();
        }
    }
}
