using System;
using System.Collections;
using Runtime.Grid;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks
{
    public class MoveableBlock : BaseBlock
    {
        [SerializeField] private float fallSpeedMultiplier = 5;
        [SerializeField] private AnimationCurve fallAnimationCurve = default;
        
        
        public override BlockType BlockType => BlockType.Moveable;
        public override bool TryOverTake(Vector3Int newPosition)
        {
            Vector3Int myPos = transform.position.ToVector3Int();
            Vector3Int direction = newPosition - myPos;
        
            BaseBlock blockAtLocation = GridManager.Instance.GetBlockAt(newPosition);
            BaseBlock floorBlockAtLocation = GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down);
            
            if (blockAtLocation != null) return false;
            if (floorBlockAtLocation == null && !CanFallAt(myPos + direction)) return false;
            return true;
        }
        
        public override bool CanBeTakenOverBy(BaseBlock baseBlock, Vector3Int direction)
        {
            BaseBlock nextBlock = GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + direction);
            
            // if we can't move to the direction we are being pushed. we cannot be overtaken
            if (!TryOverTake(transform.position.ToVector3Int() + direction))
            {
                return false;
            }
            
            switch (baseBlock.BlockType)
            {
                case BlockType.Moveable:
                case BlockType.Immovable:
                case BlockType.Breakable:
                case BlockType.Meltable:
                case BlockType.Floor:
                case BlockType.Wall:
                    return false;
                case BlockType.Player:
                    if (nextBlock != null) return false;
                    return true;
                default:
                    return false;
            }
        }

        public override bool OnGettingTakenOver(BaseBlock baseBlock, Vector3Int direction)
        {
            switch (baseBlock.BlockType)
            {
                case BlockType.Moveable:
                case BlockType.Immovable:
                case BlockType.Breakable:
                case BlockType.Meltable:
                case BlockType.Floor:
                case BlockType.Wall:
                    return false;
                case BlockType.Player:
                    transform.position += direction;
                    if (CanFallAt(transform.position.ToVector3Int())) DoFall();
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns whether or not this object should fall down. 
        /// </summary>
        /// <returns></returns>
        protected bool CanFallAt(Vector3Int location)
        {
            GridManager gridManager = GridManager.Instance;
         
            Vector3Int myPosition = location;
            BaseBlock firstFloorBlock = gridManager.GetBlockAt(myPosition + (Vector3Int.down));
            if (firstFloorBlock != null)
            {
                return false;
            }
            var baseBlock = gridManager.GetFirstBlockInDirection(myPosition + Vector3Int.down,  Vector3Int.down, 10);
            return baseBlock != null;
        }

        protected void DoFall()
        {
            GridManager gridManager = GridManager.Instance;
            Vector3Int myPosition = transform.position.ToVector3Int();
            
            var baseBlock = gridManager.GetFirstBlockInDirection(myPosition + Vector3Int.down,  Vector3Int.down, 10);
            if (baseBlock == null) return;
            
            StartCoroutine(FallAnimation(baseBlock.transform.position.ToVector3Int() + Vector3Int.up));
            
            transform.position = baseBlock.transform.position.ToVector3Int() + Vector3Int.up;
        }

        private IEnumerator FallAnimation(Vector3Int targetPos)
        {
            Vector3Int startPos = transform.position.ToVector3Int();
            float progress = 0;
            while (progress < 1)
            {
                progress = Mathf.Clamp01(progress);

                transform.position = Vector3.Lerp(startPos, targetPos, fallAnimationCurve.Evaluate(progress));
                
                progress += Time.deltaTime * fallSpeedMultiplier;
                yield return null;
            }
        }
    }
}