using System;
using System.Reflection;
using UnityEngine;

namespace Runtime.Blocks.Attributes
{
    public class DidInteractAttribute : BlockAttribute
    {
        public override bool IsValidSignature(MethodInfo methodInfo)
        {
            return IsValidSignature(methodInfo, true, typeof(Boolean),typeof(BaseBlock), typeof(Vector3Int));
        }
    }
}
