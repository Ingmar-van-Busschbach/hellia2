using System.Collections;
using System.Collections.Generic;
using Runtime.Blocks;
using Runtime.Grid;
using UnityEngine;

public class ImmovableBlock : BaseBlock
{
    public override bool CanMove(Vector3Int direction)
    {
        return false;
    }

    public override bool TryMove(Vector3Int direction)
    {
        return false;
    }

    public override bool CanBeOvertakenByType(BlockType type)
    {
        return false;
    }

    public override void HandleOvertakingBy(BlockData blockData, Vector3Int direction)
    {

    }
}
