using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Runtime.Blocks.Attributes;
using Runtime.Grid;
using UnityEngine;
using Utilities;

namespace Runtime.Blocks
{
    /// <summary>
    /// The base block is used for the floor or immovable objects and simply does not handle any interaction with it.
    /// </summary>
    public abstract class BaseBlock : MonoBehaviour
    {
        protected bool CanMove(Vector3Int direction)
        {
            if (direction == Vector3Int.zero) return false;
            BaseBlock baseBlock = GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + direction);
            if (baseBlock == null)
            {
                return CanMoveToEmpty(direction);
            }

            return InvokeCanInteract(baseBlock, direction);
        }

        protected void DoMove(Vector3Int direction)
        {
            if (direction == Vector3Int.zero) return;
            BaseBlock baseBlock = GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + direction);
            if (baseBlock == null)
            {
                DoMoveEmpty(direction);
                return;
            }

            InvokeDoInteraction(baseBlock, direction);
            InvokeDidInteraction(baseBlock, direction);
        }

        private bool InvokeCanInteract(BaseBlock baseBlock, Vector3Int direction)
        {
            var method = GetMethodBySig(baseBlock.GetType(), true, typeof(CanInteractAttribute), typeof(Boolean),
                this.GetType(), typeof(Vector3Int));
            if (method == null) return false;
 
            bool result = (bool) method.Invoke(baseBlock, new object[] {this, direction});
            return result;
        }
        
        private void InvokeDoInteraction(BaseBlock baseBlock, Vector3Int direction)
        {
            MethodInfo method = GetMethodBySig(baseBlock.GetType(), true, typeof(DoInteractAttribute), typeof(void),
                GetType(), typeof(Vector3Int));
            if (method == null) return;

            method.Invoke(baseBlock, new object[] {this, direction});
        }

        private void InvokeDidInteraction(BaseBlock baseBlock, Vector3Int direction)
        {
            var method = GetMethodBySig(GetType(), false, typeof(DidInteractAttribute), typeof(Boolean),
                baseBlock.GetType(), typeof(Vector3Int));
            if (method == null) return;

            bool shouldMove = (bool) method.Invoke(this, new object[] {baseBlock, direction});
            if (shouldMove) GridManager.Instance.Move(this, transform.position.ToVector3Int() + direction);
        }

        private bool CanMoveToEmpty(Vector3Int direction)
        {
            var method = GetMethodBySig(GetType(), true, typeof(CanInteractAttribute), typeof(Boolean),
                typeof(Vector3Int));
            if (method == null) return false;
            bool canMove = (bool) method.Invoke(this, new object[] {direction});
            return canMove;
        }

        private void DoMoveEmpty(Vector3Int direction)
        {
            var method = GetMethodBySig(GetType(), true, typeof(DidInteractAttribute), typeof(Boolean),
                typeof(Vector3Int));
            if (method == null) return;

            bool shouldMoveToEmpty = (bool) method.Invoke(this, new object[] {direction});
            if (shouldMoveToEmpty) GridManager.Instance.Move(this, transform.position.ToVector3Int() + direction);
        }

        public BaseBlock GetBlockBeneath()
        {
            return GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + Vector3Int.down);
        }

        public BaseBlock GetBlockAbove()
        {
            return GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + Vector3Int.up);
        }

        private MethodInfo GetMethodBySig(Type type, bool allowSubclass, Type attributeType, Type returnType,
            params Type[] parameterTypes)
        {
            var firstMethod = type.GetMethods().FirstOrDefault((m) =>
            {
                if (m.ReturnType != returnType) return false;
                var parameters = m.GetParameters();

                if (m.CustomAttributes.Count(data => data.AttributeType == attributeType) == 0) return false;

                if ((parameterTypes == null || parameterTypes.Length == 0)) return parameters.Length == 0;

                if (parameters.Length != parameterTypes.Length) return false;

                for (var i = parameterTypes.Length - 1; i >= 0; i--)
                {
                    if (allowSubclass && !parameters[i].ParameterType.IsSubclassOf(parameterTypes[i]))
                    {
                        if (parameters[i].ParameterType != parameterTypes[i]) return false;
                    }

                    if (!allowSubclass)
                    {
                        if (parameters[i].ParameterType != parameterTypes[i]) return false;
                    }
                }

                return true;
            });
            return firstMethod;
        }
        
    }
}