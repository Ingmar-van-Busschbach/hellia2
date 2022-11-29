using Runtime.Grid;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks
{
    public class PlayerBlock : BaseBlock
    {
        public override BlockType BlockType => BlockType.Player;
        public override bool TryOverTake(Vector3Int newPosition)
        {
            Vector3Int myPos = transform.position.ToVector3Int();
            Vector3Int direction =newPosition - myPos;

            BaseBlock block = GridManager.Instance.GetBlockAt(newPosition);
            if (block != null)
            {
                if (!block.CanBeTakenOverBy(this, direction))
                {
                    return false;
                };

                if (block.BlockType == BlockType.Climbable)
                {
                    if (block.GetBlockAbove() == null)
                    {
                        transform.position = block.transform.position.ToVector3Int() + Vector3Int.up;
                    }
                }
                block.OnGettingTakenOver(this, direction);
                return true;
            }

            // if there is no block... we can not stand on it.
            if (GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down) != null)
            {
                transform.position = newPosition;
                return true;
            } 
           
            if (GetBlockBeneath().BlockType == BlockType.Climbable)
            {
                ClimbableBlock climbableBlock = GetBlockBeneath() as ClimbableBlock;
                Vector3Int reversedDirection = new Vector3Int(-direction.x, -direction.y, -direction.z);
                
                if (!climbableBlock!.AllowedDirections.HasFlag(reversedDirection.ToDirectionsFlag()))
                {
                    return false;
                }
                if (GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down) == null)
                {
                    if (GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down + Vector3Int.down) == null)
                        return false;
                    
                    transform.position = newPosition + Vector3Int.down;
                    return true;
                }
            }

            return false;
        }

        public override bool CanBeTakenOverBy(BaseBlock baseBlock, Vector3Int direction)
        {
            return false;
        }

        public override bool OnGettingTakenOver(BaseBlock baseBlock, Vector3Int direction)
        {
            return false;
        }

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

            TryOverTake(transform.position.ToVector3Int() + direction);
        }
    }
}