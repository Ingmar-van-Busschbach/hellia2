using System;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;

namespace Runtime.Blocks.Attributes
{
    public class DoInteractAttribute : BlockAttribute
    {
        public override bool IsValidSignature(MethodInfo methodInfo)
        {
            return IsValidSignature(methodInfo, true, typeof(void), typeof(BaseBlock), typeof(Vector3Int));
        }
    }
}
