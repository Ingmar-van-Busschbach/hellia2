using Runtime.Grid;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks
{
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
            return floorBlockAtLocation != null;
        }

        public override bool CanBeTakenOverBy(BaseBlock baseBlock, Vector3Int direction)
        {
            return false;
        }

        protected override bool TakeOver(BaseBlock baseBlock, Vector3Int direction)
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
            Debug.LogWarning(cameraDirection);

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

            if (CanMoveTo(transform.position.ToVector3Int() + direction))
            {
                Debug.Log("Can move succesfull");
                TryMove(transform.position.ToVector3Int() + direction, false);
            }
        }
    }
}