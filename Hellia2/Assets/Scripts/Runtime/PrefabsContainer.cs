using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Blocks;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BlockPrefabs", order = 1)]
public class PrefabsContainer : ScriptableObject
{
    [SerializeField] private BaseBlock playerPrefab;
    [SerializeField] private BaseBlock defaultFloorBlock;
    [SerializeField] private BaseBlock[] moveablePrefabs;
    [SerializeField] private BaseBlock[] immovablePrefabs;
    [SerializeField] private BaseBlock[] breakablePrefabs;
    [SerializeField] private BaseBlock[] meltablePrefabs;
    [SerializeField] private BaseBlock[] floorPrefabs;
    [SerializeField] private BaseBlock[] wallPrefabs;
    [SerializeField] private BaseBlock[] holePrefabs;

    public BaseBlock[] HolePrefabs => holePrefabs;
    public BaseBlock[] MoveablePrefabs => moveablePrefabs;
    public BaseBlock[] ImmovablePrefabs => immovablePrefabs;
    public BaseBlock[] BreakablePrefabs => breakablePrefabs;
    public BaseBlock[] MeltablePrefabs => meltablePrefabs;
    public BaseBlock[] FloorPrefabs => floorPrefabs;
    public BaseBlock[] WallPrefabs => wallPrefabs;

    public BaseBlock DefaultFloorBlock => defaultFloorBlock;
    public BaseBlock PlayerPrefab => playerPrefab;
}