using Runtime.Blocks;
using Runtime.LevelCreation;
using UnityEngine;

namespace Runtime
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BlockPrefabs", order = 1)]
    public class PrefabsContainer : ScriptableObject
    {
        [SerializeField] private BuildBlockData playerPrefab;
        [SerializeField] private BuildBlockData defaultFloorBlock;
        [SerializeField] private BuildBlockData[] moveablePrefabs;
        [SerializeField] private BuildBlockData[] immovablePrefabs;
        [SerializeField] private BuildBlockData[] breakablePrefabs;
        [SerializeField] private BuildBlockData[] meltablePrefabs;
        [SerializeField] private BuildBlockData[] floorPrefabs;
        [SerializeField] private BuildBlockData[] wallPrefabs;
        [SerializeField] private BuildBlockData[] climbablePrefabs;

        public BuildBlockData[] ClimbablePrefabs => climbablePrefabs;
        public BuildBlockData[] MoveablePrefabs => moveablePrefabs;
        public BuildBlockData[] ImmovablePrefabs => immovablePrefabs;
        public BuildBlockData[] BreakablePrefabs => breakablePrefabs;
        public BuildBlockData[] MeltablePrefabs => meltablePrefabs;
        public BuildBlockData[] FloorPrefabs => floorPrefabs;
        public BuildBlockData[] WallPrefabs => wallPrefabs;

        public BuildBlockData DefaultFloorBlock => defaultFloorBlock;
        public BuildBlockData PlayerPrefab => playerPrefab;
    }
}
