using Runtime.Blocks.Attributes;
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

            Vector3Int cameraDirection =
                (this.transform.position - Camera.main.transform.position).normalized.ToVector3Int();

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

            if (CanMove(direction))
            {
                DoMove(direction);
            }
        }


        [DidInteract]
        public bool DidInteractWithMoveAble(MoveableBlock block, Vector3Int direction)
        {
            Debug.Log("Player interacted with a MoveAble block");
            return false;
        }

        [DidInteract]
        public bool DidInteractWithClimbable(ClimbableBlock climbableBlock, Vector3Int direction)
        {
            if (climbableBlock.transform.position.ToVector3Int() == transform.position.ToVector3Int() + Vector3Int.down)
            {
                transform.position = climbableBlock.transform.position.ToVector3Int() + Vector3Int.up;
            }
            else
            {
                transform.position = climbableBlock.transform.position.ToVector3Int() + Vector3Int.up;
            }
            return false;
        }

        [CanInteract]
        public bool CanInteractWithEmptySpace(Vector3Int direction)
        {
            Vector3Int newPosition = transform.position.ToVector3Int() + direction;

            // there is no block we can standing... but can we climb down the block we are standing on?
            if (GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down) != null)
            {
                return true;
            }
            // if there is also no block beneath that we can not climb down... as we would fall down.
            if (GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down * 2) == null)
            {
                return false;
            }
            
            ClimbableBlock climbableBlock = GetBlockBeneath() as ClimbableBlock;
            if (climbableBlock == null) return false;
            Vector3Int reversedDirection = new Vector3Int(-direction.x, -direction.y, -direction.z);
            return climbableBlock!.CanBeInteractedByPlayer(this, reversedDirection);
        }

        [DidInteract]
        public bool DidInteract(Vector3Int direction)
        {
            Vector3Int newPosition = transform.position.ToVector3Int() + direction;
         
            if (GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down) != null)
            {
                return true;
            }
            // if there is also no block beneath that we can not climb down... as we would fall down.
            if (GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down * 2) == null)
            {
                return false;
            }
            
            ClimbableBlock climbableBlock = GetBlockBeneath() as ClimbableBlock;
            if (climbableBlock == null) return false;
            transform.position = climbableBlock.transform.position.ToVector3Int() + direction;
            return false;
        }
    }
}