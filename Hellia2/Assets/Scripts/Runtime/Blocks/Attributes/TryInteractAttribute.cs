using System;
using System.Reflection;
using UnityEngine;

namespace Runtime.Blocks.Attributes
{
    public class TryInteractAttribute : BlockAttribute
    {
        private readonly Type _blockType;

        public TryInteractAttribute(Type blockType)
        {
            _blockType = blockType;
        }
        
        public override bool IsValidSignature(MethodInfo methodInfo)
        {
            return IsValidSignature(methodInfo, typeof(Boolean), _blockType, typeof(Vector3Int));

        }
    }
}
