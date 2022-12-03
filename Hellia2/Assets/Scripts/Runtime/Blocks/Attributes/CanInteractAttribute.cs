using System;
using System.Reflection;
using JetBrains.Annotations;
using Runtime.Grid;
using UnityEngine;

namespace Runtime.Blocks.Attributes
{
    public class CanInteractAttribute : BlockAttribute
    {
        public override bool IsValidSignature(MethodInfo methodInfo)
        {
            return IsValidSignature(methodInfo, true, typeof(Boolean), typeof(BaseBlock), typeof(Vector3Int));
        }
    }
}
