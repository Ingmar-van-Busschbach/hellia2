using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Runtime.Grid
{
    public class MapData
    {
        private readonly Dictionary<Vector3Int, BlockData> _map = new();

        public BlockData GetBlockAt(Vector3Int location)
        {
            if (!_map.ContainsKey(location))
            {
                var emptyBlockData = new BlockData {direction = Direction.None, type = BlockType.Empty};
                return emptyBlockData;
            }

            return _map[location];
        }

        public void SetBlockAt(Vector3Int location, BlockData blockData)
        {
            _map[location] = blockData;
        }

        public void Load(string mapName)
        {
            if (!PlayerPrefs.HasKey(mapName))
            {
                Debug.LogWarning($"Trying to load map ${mapName} but the map does not exist.");
                return;
            }
            
            var data = JsonConvert.DeserializeObject<Dictionary<string, BlockData>>(PlayerPrefs.GetString(mapName));
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

        public void Save(string mapName)
        {
            PlayerPrefs.SetString(mapName, JsonConvert.SerializeObject(_map));
            PlayerPrefs.Save();
            Debug.LogWarning("Saved " + JsonConvert.SerializeObject(_map));
        }
    }
}