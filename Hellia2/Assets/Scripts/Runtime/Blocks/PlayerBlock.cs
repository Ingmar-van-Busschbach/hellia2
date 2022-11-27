using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Blocks;
using Runtime.Grid;
using UnityEngine;
using Utilities;

public class PlayerBlock : BaseBlock
{
    private void Update()
    {
        Vector3Int myPos = transform.position.ToVector3Int();
        Vector3Int direction = Vector3Int.zero;
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector3Int.forward;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector3Int.left;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector3Int.back;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector3Int.right;
        }

        TryMove(direction);
    }

    public override bool CanMove(Vector3Int direction)
    {
        Vector3Int targetPos = transform.position.ToVector3Int() + direction;
        BlockData targetBlockData = GridManager.Instance.MapData.GetBlockDataAt(targetPos);

        switch (targetBlockData.type)
        {
            case BlockType.Immovable:
            case BlockType.Breakable:
            case BlockType.Meltable:
            case BlockType.Floor:
            case BlockType.Wall:
            case BlockType.Player:
            case BlockType.Hole:
                return false;
            case BlockType.Empty:
            case BlockType.Moveable:
                return true;
            default:
                return false;
        }
    }

    public override bool TryMove(Vector3Int direction)
    {
        Vector3Int targetPos = transform.position.ToVector3Int() + direction;
        BlockData targetBlockData = GridManager.Instance.MapData.GetBlockDataAt(targetPos);
        BlockData floorBlockData = GridManager.Instance.MapData.GetBlockDataAt(targetPos - Vector3Int.down);
        BaseBlock targetBlock = GridManager.Instance.GetBlockAt(targetPos);
        
        Debug.LogWarning(targetBlockData.type + " : " + floorBlockData.type + " : " + direction);

        switch (targetBlockData.type)
        {
            case BlockType.Moveable:
            case BlockType.Immovable:
            case BlockType.Breakable:
            case BlockType.Meltable:
            case BlockType.Floor:
            case BlockType.Wall:
            case BlockType.Player:
            case BlockType.Hole:
                return false;
            case BlockType.Empty:
                if (floorBlockData.type is BlockType.Empty or not BlockType.Hole) return false;
                Debug.Log("yes");
                transform.position += direction;
                return true;
            default:
                return false;
        }

        return false;
    }

    public override bool CanBeOvertakenByType(BlockType type)
    {
        throw new NotImplementedException();
    }

    public override void HandleOvertakingBy(BlockData blockData, Vector3Int direction)
    {
        throw new NotImplementedException();
    }
}