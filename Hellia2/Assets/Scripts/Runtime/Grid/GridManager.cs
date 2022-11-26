using System;
using UnityEngine;

namespace Runtime.Grid
{
    public class GridManager : MonoBehaviour
    {
        private MapData _mapData;

        private void Awake()
        {
            _mapData = new MapData();
        }
    }
}