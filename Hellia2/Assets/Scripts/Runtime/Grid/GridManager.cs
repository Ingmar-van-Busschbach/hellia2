using System.Linq;
using Runtime.Blocks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Runtime.Grid
{
    public class GridManager : SingletonMonoBehaviour<GridManager>
    {
        private BaseBlock[] _blocks;

        private void Awake()
        {
            _blocks = FindObjectsOfType<BaseBlock>();
        }

        public BaseBlock GetBlockAt(Vector3Int location)
        {
            return _blocks.FirstOrDefault(block => block.transform.position.ToVector3Int() == location);
        }

        public BaseBlock GetFirstBlockInDirection(Vector3Int startPos, Vector3Int direction, int maxDistance)
        {
            for (int i = 0; i < maxDistance; i++)
            {
                BaseBlock block = GetBlockAt(startPos + (direction * i));
                if (block == null) continue;
                return block;
            }

            return null;
        } 
    }
}