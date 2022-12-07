using Runtime.Blocks.LightBlocks;
using Runtime.Grid;

namespace Runtime.Blocks
{
    public class MeltableBlock : BaseBlock, ILightReceiver
    {
        public void ReceiveLight()
        {
            GridManager.Instance.DestroyBlock(this);
        }
    }
}
