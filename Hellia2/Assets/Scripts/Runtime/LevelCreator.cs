using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Runtime.Blocks;
using Runtime.Grid;
using Runtime.LevelCreation;
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

        private const int DefaultFloorSize = 5;
        private const int DefaultFloorHeight = 10;

        private List<GameObject> _spawnedObjects = new();
        
        public void SpawnDefaultFloor()
        {
            if (prefabsContainer.DefaultFloorBlock.blockPrefab == null)
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
        }

        public void PlaceBlockAt(Vector3Int position, BuildBlockData buildBlockData)
        {
            GameObject spawnedObj = PrefabUtility.InstantiatePrefab(buildBlockData.blockPrefab) as GameObject;
            if (spawnedObj == null) return;
            
            spawnedObj.transform.position = position;
            spawnedObj.transform.parent = transform;
            spawnedObj.isStatic = buildBlockData.isStatic;
            spawnedObj.name = $"{spawnedObj.transform.position.ToVector3Int()} : {spawnedObj.name}";
            _spawnedObjects.Add(spawnedObj);
        }
        
        public void DestroyBlock(Vector3Int location)
        {
            if (_spawnedObjects.Count == 0) _spawnedObjects = FindObjectsOfType<BaseBlock>().Select(block => block.gameObject).ToList();
            GameObject targetObj = _spawnedObjects.FirstOrDefault(o => o.transform.position.ToVector3Int() == location);
            if (targetObj == null) return;
            _spawnedObjects.Remove(targetObj);
            DestroyImmediate(targetObj);
        }
        
        public void DestroyCurrentMap()
        {
            for (int i = _spawnedObjects.Count-1; i >= 0; i--)
            {
                DestroyImmediate(_spawnedObjects[i]);
            }
            _spawnedObjects.Clear();
        }
        
        public PrefabsContainer PrefabsContainer => prefabsContainer;
    }
}
