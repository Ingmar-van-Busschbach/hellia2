using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Grid;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Runtime
{
    [System.Serializable]
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private GameObject defaultFloor;

        private MapData _mapData;
        
        private const int DefaultFloorSize = 5;
        private const int DefaultFloorHeight = 10;

        private List<GameObject> _spawnedObjects = new();
        
        public void SpawnDefaultFloor()
        {
            for (int i = 0; i < DefaultFloorSize; i++)
            {
                for (int j = 0; j < DefaultFloorSize; j++)
                {
                    PlaceFloorBlock(new Vector3Int(i, DefaultFloorHeight, j));
                }
            }
        }

        public void CreateNewMapData()
        {
            transform.position = Vector3.zero;
            _mapData = new MapData();
        }

        public void PlaceFloorBlock(Vector3Int position)
        {
            GameObject spawnedObj = Instantiate(defaultFloor, position, Quaternion.identity);
            spawnedObj.transform.parent = transform;
            _spawnedObjects.Add(spawnedObj);
            
            _mapData.SetBlockAt(position, new BlockData()
            {
                direction = Direction.None,
                type = BlockType.Floor,
            });
        }

        public void DestroyBlock(Vector3Int location)
        {
            GameObject targetObj = _spawnedObjects.FirstOrDefault(o => o.transform.position.ToVector3Int() == location);
            if (targetObj == null) return;
            _mapData.RemoveBlockAt(location);
            _spawnedObjects.Remove(targetObj);
            DestroyImmediate(targetObj);
        }
        
        public void DestroyCurrentMap()
        {
            _mapData = null;
            for (int i = _spawnedObjects.Count-1; i >= 0; i--)
            {
                DestroyImmediate(_spawnedObjects[i]);
            }
            _spawnedObjects.Clear();
        }
        
        public void SaveLevel()
        {
            if (_mapData != null)
            {
                _mapData.Save(SceneManager.GetActiveScene().name);
            }
        }

        public MapData MapData => _mapData;
    }
}
