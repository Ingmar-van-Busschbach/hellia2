using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Blocks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utilities;

namespace Runtime.Grid
{
    public class GridManager : SingletonMonoBehaviour<GridManager>
    {
        private Dictionary<Vector3Int, BaseBlock> _blocks = new();

        public UnityEvent onBlockMoved = new();

        private void Awake()
        {
            foreach (var baseBlock in FindObjectsOfType<BaseBlock>())
            {
                _blocks.Add(baseBlock.transform.position.ToVector3Int(), baseBlock);
                baseBlock.transform.hasChanged = false;
            }
        }

        public void Move(BaseBlock block, Vector3Int newPosition)
        {
            Vector3Int oldPosition = block.transform.position.ToVector3Int();
            var value = _blocks[oldPosition];
            _blocks.Remove(oldPosition);
            _blocks[newPosition] = value;
            block.transform.position = newPosition;
            onBlockMoved?.Invoke();
        }
        
        public BaseBlock GetBlockAt(Vector3Int location)
        {
            return _blocks.ContainsKey(location) ? _blocks[location] : null;
        }

        public BaseBlock GetFirstBlockInDirection(Vector3Int startPos, Vector3Int direction, int maxDistance)
        {
            return GetFirstBlockOfTypeInDirection<BaseBlock>(startPos, direction, maxDistance);
        } 
        
        public T GetFirstBlockOfTypeInDirection<T>(Vector3Int startPos, Vector3Int direction, int maxDistance) where T : BaseBlock
        {
            for (int i = 0; i < maxDistance; i++)
            {
                BaseBlock block = GetBlockAt(startPos + (direction * i));
                if (block == null) continue;
                if (!block.GetType().IsSubclassOf(typeof(T))) continue;
                return block as T;
            }

            return null;
        } 


        public List<BaseBlock> GetAllBlocksInDirection(Vector3Int startPos, Vector3Int direction, int maxDistance)
        {
            return GetAllBlocksOfTypeInDirection<BaseBlock>(startPos, direction, maxDistance);
        }

        public List<T> GetAllBlocksOfTypeInDirection<T>(Vector3Int startPos, Vector3Int direction, int maxDistance) where T : BaseBlock
        {
            List<T> result = new();
            for (int i = 0; i < maxDistance; i++)
            {
                BaseBlock block = GetBlockAt(startPos + (direction * i));
                if (block == null) continue;
                if (block.GetType() != typeof(T)) continue;
                result.Add(block as T);
            }

            return result;
        }
    }
}