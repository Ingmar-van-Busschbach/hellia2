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
            // custom move logic.
            transform.position = climbableBlock.transform.position.ToVector3Int() + Vector3Int.up;
            return false;
        }
    }
}