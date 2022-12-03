using System;
using System.Reflection;
using UnityEngine;

namespace Runtime.Blocks.Attributes
{
    /// <summary>
    /// Can be added to methodes on a block class. Where it will call this function if the block tried to interact
    /// with a specific other block.
    ///
    /// if the other block is empty it will call all functions where to block type is specified.
    /// </summary>
    public class DidInteractAttribute : BlockAttribute
    {
        public override bool IsValidSignature(MethodInfo methodInfo)
        {
            bool valid1 = IsValidSignature(methodInfo, true, typeof(Boolean),typeof(BaseBlock), typeof(Vector3Int));
            bool valid2 = IsValidSignature(methodInfo, true, typeof(Boolean), typeof(Vector3Int));
            return valid1 || valid2;
        }
    }
}
