using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Blocks;
using Runtime.Grid;
using UnityEngine;
using Utilities;

public class PlayerBlock : BaseBlock
{
    public override BlockType BlockType => BlockType.Player;
    protected override bool CanMoveTo(Vector3Int newPosition)
    {
        Vector3Int myPos = transform.position.ToVector3Int();
        Vector3Int direction = newPosition - myPos;
        
        BaseBlock blockAtLocation = GridManager.Instance.GetBlockAt(newPosition);
        BaseBlock floorBlockAtLocation = GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down);
        
        if (blockAtLocation != null)
        {
            if (!blockAtLocation.CanBeTakenOverBy(this, direction))
            {
                return false;
            }
        }

        if (floorBlockAtLocation == null || floorBlockAtLocation.BlockType == BlockType.Hole)
        {
            return false;
        }

        return true;
    }

    public override bool CanBeTakenOverBy(BaseBlock baseBlock, Vector3Int direction)
    {
        return false;
    }

    protected override bool TakeOver(BaseBlock baseBlock, Vector3Int direction)
    {
        throw new NotImplementedException();
    }

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

        if (CanMoveTo(transform.position.ToVector3Int() + direction))
        {
            Debug.Log("Can move succesfull");
            TryMove(transform.position.ToVector3Int() + direction, false);
        }
    }
}