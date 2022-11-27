using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Runtime.Grid
{
    [System.Serializable]
    public class MapData
    {
        private readonly Dictionary<Vector3Int, BlockData> _map = new();

        public BlockData GetBlockDataAt(Vector3Int location)
        {
            if (!_map.ContainsKey(location))
            {
                var emptyBlockData = new BlockData {direction = Direction.None, type = BlockType.Empty};
                return emptyBlockData;
            }

            return _map[location];
        }

        public void SetBlockDataAt(Vector3Int location, BlockData blockData)
        {
            _map[location] = blockData;
        }

        public void RemoveBlockDataAt(Vector3Int location)
        {
            _map.Remove(location);
        }

        public Vector3Int? GetPositionOf(BlockData blockData)
        {
            if (!_map.ContainsValue(blockData)) return null;
            return _map.Keys.FirstOrDefault(location => _map[location].Equals(blockData));
        }
        
        #region Saving and loading

        public void Load(string mapName)
        {
            string assetsPath = Application.dataPath;
            assetsPath += $"/Data/Levels/{mapName}.txt";
            
            if (!System.IO.File.Exists(assetsPath))
            {
                Debug.LogWarning($"Trying to load map ${mapName} but the map does not exist.");
                return;
            }

            var data = JsonConvert.DeserializeObject<Dictionary<string, BlockData>>(System.IO.File.ReadAllText(assetsPath));
            if (data == null)
            {
                Debug.LogWarning($"Trying to load map ${mapName} but the data is invalid");
                return;
            }

            _map.Clear();
            foreach (var (key, value) in data)
            {
                string positionString = key.Replace("(", "");
                positionString = positionString.Replace(")", "");

                int positionX = int.Parse(positionString.Split(",")[0].Replace(",", ""));
                int positionY = int.Parse(positionString.Split(",")[1].Replace(",", ""));
                int positionZ = int.Parse(positionString.Split(",")[2].Replace(",", ""));
                _map[new Vector3Int(positionX, positionY, positionZ)] = value;
            }
        }

#if (UNITY_EDITOR)
        public void Save(string mapName)
        {
            string assetsPath = Application.dataPath;
            assetsPath += $"/Data/Levels/{mapName}.txt";

            if (!System.IO.Directory.Exists($"{Application.dataPath}/Data"))
            {
                System.IO.Directory.CreateDirectory($"{Application.dataPath}/Data");
            }
            if (!System.IO.Directory.Exists($"{Application.dataPath}/Data/levels"))
            {
                System.IO.Directory.CreateDirectory($"{Application.dataPath}/Data/Levels");
            }
            
            System.IO.File.WriteAllText(assetsPath, JsonConvert.SerializeObject(_map));
            Debug.LogWarning("Saved " + JsonConvert.SerializeObject(_map));
        }
#endif
        #endregion
    }
}