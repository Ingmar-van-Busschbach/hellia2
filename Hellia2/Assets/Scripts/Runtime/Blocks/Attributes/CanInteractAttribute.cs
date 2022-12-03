using System;
using System.Reflection;
using JetBrains.Annotations;
using Runtime.Grid;
using UnityEngine;

namespace Runtime.Blocks.Attributes
{
    /// <summary>
    /// Called on the specific block your trying to overtake. unless the block is empty. then the function is called on the executing block.
    /// Which can decide whether or not it can overtake an empty space.
    ///
    /// If the block space if empty it will call the function in the executing class where no block type is specified.
    /// </summary>
    public class CanInteractAttribute : BlockAttribute
    {
        public override bool IsValidSignature(MethodInfo methodInfo)
        {
            bool valid1 = IsValidSignature(methodInfo, true, typeof(Boolean), typeof(BaseBlock), typeof(Vector3Int));
            bool valid2 = IsValidSignature(methodInfo, true, typeof(Boolean), typeof(Vector3Int));
            return valid1 || valid2;
        }
    }
}
