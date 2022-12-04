using System.Linq;
using Runtime.Data;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks.LightBlocks
{
    public class LightSplitter : BaseLightBlock
    {
        protected override bool CanEmitTo(Directions directions)
        {
            bool defaultCanEmit = base.CanEmitTo(directions);
            if (!defaultCanEmit) return false;

            if (!(ReceivingFrom.Any(pair => pair.Key.ToDirectionsFlag() != Directions.Nothing))) return false;
            
            return !ReceivingFrom.Any(keyValuePair => directions.HasFlag(keyValuePair.Key.ToDirectionsFlag().Flip()));
        }
        
        
        protected override LightEmitData EmitLight(Vector3Int direction)
        {
            LightEmitData data = base.EmitLight(direction);
            RenderLight(data);
            return data;
        }

        public void RenderLight(LightEmitData data)
        {
            Debug.DrawLine(transform.position.ToVector3Int(), data.HitPosition, Color.red);
        }
    }
}
