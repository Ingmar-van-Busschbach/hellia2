using System;
using Runtime.Blocks.Attributes;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks
{
    public class ClimbableBlock : BaseBlock
    {
        [SerializeField] private Directions allowedDirections;

        [CanInteract]
        public bool CanBeInteractedByPlayer(PlayerBlock playerBlock, Vector3Int direction)
        {
            return allowedDirections.HasFlag(direction.ToDirectionsFlag());
        }

        [DoInteract]
        public void OnPlayerInteracted(PlayerBlock playerBlock, Vector3Int direction)
        {
        }
    }
}
