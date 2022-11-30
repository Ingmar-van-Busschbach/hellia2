using Runtime.Blocks;
using UnityEngine;

public interface IClimbable
{
    public bool CanClimbUp(BaseBlock block, Vector3Int direction);
    public bool CanClimbDown(BaseBlock block, Vector3Int direction);
}
