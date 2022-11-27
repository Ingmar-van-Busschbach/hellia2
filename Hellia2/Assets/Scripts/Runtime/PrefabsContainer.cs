using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BlockPrefabs", order = 1)]
public class PrefabsContainer : ScriptableObject
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject defaultFloorBlock;
    [SerializeField] private GameObject[] moveablePrefabs;
    [SerializeField] private GameObject[] immovablePrefabs;
    [SerializeField] private GameObject[] breakablePrefabs;
    [SerializeField] private GameObject[] meltablePrefabs;
    [SerializeField] private GameObject[] floorPrefabs;
    [SerializeField] private GameObject[] wallPrefabs;
    [SerializeField] private GameObject[] holePrefabs;

    public GameObject[] HolePrefabs => holePrefabs;
    public GameObject[] MoveablePrefabs => moveablePrefabs;
    public GameObject[] ImmovablePrefabs => immovablePrefabs;
    public GameObject[] BreakablePrefabs => breakablePrefabs;
    public GameObject[] MeltablePrefabs => meltablePrefabs;
    public GameObject[] FloorPrefabs => floorPrefabs;
    public GameObject[] WallPrefabs => wallPrefabs;

    public GameObject DefaultFloorBlock => defaultFloorBlock;
    public GameObject PlayerPrefab => playerPrefab;
}
