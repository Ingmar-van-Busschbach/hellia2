using System;
using System.Reflection;
using Runtime.Grid;
using UnityEngine;

namespace Runtime.Blocks.Attributes
{
    public class CanInteractAttribute : BlockAttribute
    {
        private readonly Type _blockType;

        public CanInteractAttribute(Type blockType)
        {
            _blockType = blockType;
        }
        public override bool IsValidSignature(MethodInfo methodInfo)
        {
            return IsValidSignature(methodInfo, typeof(Boolean), _blockType, typeof(Vector3Int));
        }
    }
}
