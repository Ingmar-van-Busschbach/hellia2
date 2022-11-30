using UnityEngine;

namespace Runtime.Blocks.BlockInterfaces
{
    public interface IFallable
    {
        public bool CanFallAt(Vector3Int fallLocation);
        public void DoFall();
    }
}
