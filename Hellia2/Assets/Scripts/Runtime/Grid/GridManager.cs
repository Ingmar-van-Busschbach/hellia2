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
    }
}