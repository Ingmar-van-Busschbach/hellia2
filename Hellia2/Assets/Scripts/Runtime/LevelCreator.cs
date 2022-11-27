using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Grid;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Runtime
{
    [System.Serializable]
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private PrefabsContainer prefabsContainer;

        private MapData _mapData;
        
        private const int DefaultFloorSize = 5;
        private const int DefaultFloorHeight = 10;

        private List<GameObject> _spawnedObjects = new();
        
        public void SpawnDefaultFloor()
        {
            if (prefabsContainer.DefaultFloorBlock == null)
            {
                Debug.LogWarning("There is no floor block, cannot spawn the default floor.");
                return;
            }
            for (int i = 0; i < DefaultFloorSize; i++)
            {
                for (int j = 0; j < DefaultFloorSize; j++)
                {
                    PlaceBlockAt(new Vector3Int(i, DefaultFloorHeight, j), prefabsContainer.DefaultFloorBlock);
                }
            }
        }

        public void CreateNewMapData()
        {
            transform.position = Vector3.zero;
            _mapData = new MapData();
        }

        public void PlaceBlockAt(Vector3Int position, GameObject block, BlockType blockType = BlockType.Floor, Direction direction = Direction.None)
        {
            if (_mapData == null) return;
            
            if (_mapData.GetBlockDataAt(position).type != BlockType.Empty) return;
            
            GameObject spawnedObj = PrefabUtility.InstantiatePrefab(block) as GameObject;
            if (spawnedObj == null) return;
            
            spawnedObj.transform.position = position;
            spawnedObj.transform.parent = transform;
            _spawnedObjects.Add(spawnedObj);
            
            Debug.Log($"Placed block of type: ${blockType}");
            _mapData.SetBlockDataAt(position, new BlockData()
            {
                direction = direction,
                type = blockType,
            });
        }

        public void DestroyBlock(Vector3Int location)
        {
            GameObject targetObj = _spawnedObjects.FirstOrDefault(o => o.transform.position.ToVector3Int() == location);
            if (targetObj == null) return;
            _mapData.RemoveBlockDataAt(location);
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

        public PrefabsContainer PrefabsContainer => prefabsContainer;
    }
}
