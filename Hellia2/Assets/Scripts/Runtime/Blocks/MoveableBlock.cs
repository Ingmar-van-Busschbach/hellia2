using System;
using System.Collections;
using Runtime.Blocks.Attributes;
using Runtime.Grid;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks
{
    public class MoveableBlock : BaseBlock
    {
        [SerializeField] private float fallSpeedMultiplier = 5;
        [SerializeField] private AnimationCurve fallAnimationCurve = default;


        [CanInteract]
        public bool CanBePushedByPlayer(PlayerBlock playerBlock, Vector3Int direction)
        {
            Vector3Int newPosition = transform.position.ToVector3Int() + direction;
            BaseBlock blockAtLocation = GridManager.Instance.GetBlockAt(newPosition);
            BaseBlock floorBlockAtLocation = GridManager.Instance.GetBlockAt(newPosition + Vector3Int.down);
            
            if (blockAtLocation != null) return false;
            if (floorBlockAtLocation == null && !CanFallAt(newPosition)) return false;
            return true;
        }

        [DoInteract]
        public void OnInteractedByPlayer(PlayerBlock playerBlock, Vector3Int direction)
        {
            transform.position += direction;
            if (CanFallAt(transform.position.ToVector3Int())) DoFall();
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