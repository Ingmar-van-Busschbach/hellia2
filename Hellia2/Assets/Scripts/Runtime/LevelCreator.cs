using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Blocks;
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

        private const int DefaultFloorSize = 5;
        private const int DefaultFloorHeight = 10;
        
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
                    PlaceBlockAt(new Vector3Int(i, DefaultFloorHeight, j), prefabsContainer.DefaultFloorBlock.gameObject);
                }
            }
        }

        public void CreateNewMapData()
        {
            transform.position = Vector3.zero;
        }

        public void PlaceBlockAt(Vector3Int position, GameObject block, BlockType blockType = BlockType.Floor, Directions directions = Directions.Nothing)
        {
           
            GameObject spawnedObj = PrefabUtility.InstantiatePrefab(block) as GameObject;
            if (spawnedObj == null) return;
            
            spawnedObj.transform.position = position;
            spawnedObj.transform.parent = transform;
        }

        public void DestroyBlock(Vector3Int location)
        {
            BaseBlock baseBlock = GridManager.Instance.GetBlockAt(location);
            if (baseBlock == null) return;
            DestroyImmediate(baseBlock);
        }

        public PrefabsContainer PrefabsContainer => prefabsContainer;
    }
}
