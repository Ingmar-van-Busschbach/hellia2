using System;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;

namespace Runtime.Blocks.Attributes
{
    /// <summary>
    /// Called on the block that is getting interacted. when a block tries to interact it will call this function on the
    /// other block to notify that it has been interacted.
    /// </summary>
    public class DoInteractAttribute : BlockAttribute
    {
        public override bool IsValidSignature(MethodInfo methodInfo)
        {
            return IsValidSignature(methodInfo, true, typeof(void), typeof(BaseBlock), typeof(Vector3Int));
        }
    }
}
