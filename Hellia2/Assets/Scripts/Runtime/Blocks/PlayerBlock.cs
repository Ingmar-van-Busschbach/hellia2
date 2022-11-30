using System;
using Runtime.Blocks.BlockInterfaces;
using Runtime.Grid;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks
{
    public class PlayerBlock : BaseBlock
    {
        public override BlockType BlockType => BlockType.Player;

        private void Update()
        {
            Camera currentCamera = Camera.current;

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

            Vector3Int cameraDirection = (this.transform.position - Camera.main.transform.position).normalized.ToVector3Int();

            if (cameraDirection.z == -1)
            {
                direction = new Vector3Int(-direction.x, -direction.y, -direction.z);
            }

            if (cameraDirection.x == 1)
            {
                direction = new Vector3Int(direction.z, direction.y, -direction.x);
            }

            if (cameraDirection.x == -1)
            {
                direction = new Vector3Int(-direction.z, direction.y, direction.x);
            }
            
            // try move in direction
            TryMove(direction);
        }

        private void TryMove(Vector3Int direction)
        {
            Vector3Int myPos = transform.position.ToVector3Int();
            BaseBlock targetBlock = GridManager.Instance.GetBlockAt(myPos + direction);
            BaseBlock targetFloorBlock = GridManager.Instance.GetBlockAt(myPos + direction + Vector3Int.down);
            BaseBlock targetTopBlock = GridManager.Instance.GetBlockAt(myPos + direction + Vector3Int.up);

            if (targetBlock == null)
            {
                if (targetFloorBlock != null)
                {
                    transform.position = myPos + direction;
                    return;
                }
                if (GetBlockBeneath().BlockType != BlockType.Climbable) return;
                IClimbable climbable = GetBlockBeneath() as IClimbable;
                if (climbable!.CanClimbDown(this, direction))
                {
                    transform.position = myPos + direction + Vector3.down;
                }

                return;
            }
            switch (targetBlock == null ? BlockType.Player : targetBlock.BlockType)
            {
                case BlockType.Moveable:
                    IPushable pushable = targetBlock as IPushable;
                    if (pushable!.CanPush(this, direction))
                    {
                        pushable.Push(this, direction);
                    }
                    break;
                case BlockType.Climbable:
                    IClimbable climbable = targetBlock as IClimbable;
                    if (climbable!.CanClimbUp(this, direction))
                    {
                        if (targetTopBlock == null)
                        {
                            transform.position = myPos + direction + Vector3.up;
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}