using System.Linq;
using Runtime.Blocks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Runtime.Grid
{
    public class GridManager : SingletonMonoBehaviour<GridManager>
    {
        private MapData _mapData;

        private void Awake()
        {
            _mapData = new MapData();
            _mapData.Load(SceneManager.GetActiveScene().name);
        }

        public bool TryMoveTo(Vector3Int myPos, Vector3Int targetPos)
        {
            BaseBlock myBlock = GetBlockAt(myPos);
            BaseBlock targetBlock = GetBlockAt(targetPos);

            BlockData myBlockData = MapData.GetBlockDataAt(myPos);
            BlockData targetBlockData = MapData.GetBlockDataAt(targetPos);
            
            return false;
        }

        public BaseBlock GetBlockAt(Vector3Int vector3Int)
        {
            return FindObjectsOfType<BaseBlock>().FirstOrDefault(data => data.transform.position == vector3Int);
        }

        public MapData MapData => _mapData;
    }
}