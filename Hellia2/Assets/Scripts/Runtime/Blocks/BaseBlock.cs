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
                var voidBlockMethodes = GetMethodsBySig(GetType(), true, typeof(CanInteractAttribute), typeof(Boolean),
                    typeof(Vector3Int));
                var voidBlockInfos = voidBlockMethodes as MethodInfo[] ?? voidBlockMethodes.ToArray();
                if (voidBlockInfos.ToArray().Length == 0) return false;
                bool canMove = (bool) voidBlockInfos.First().Invoke(this, new object[] {direction});
                return canMove;
            }

            var methodes = GetMethodsBySig(baseBlock.GetType(), true, typeof(CanInteractAttribute), typeof(Boolean),
                this.GetType(), typeof(Vector3Int));

            var methodInfos = methodes as MethodInfo[] ?? methodes.ToArray();
            if (methodInfos.ToArray().Length == 0) return false;

            bool result = (bool) methodInfos.First().Invoke(baseBlock, new object[] {this, direction});
            return result;
        }

        protected void DoMove(Vector3Int direction)
        {
            if (direction == Vector3Int.zero) return;
            BaseBlock baseBlock = GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + direction);
            if (baseBlock == null)
            {
                var voidBlockMethodes = GetMethodsBySig(GetType(), true, typeof(DidInteractAttribute), typeof(Boolean),
                    typeof(Vector3Int));

                var voidBlockInfos = voidBlockMethodes as MethodInfo[] ?? voidBlockMethodes.ToArray();

                if (voidBlockInfos.ToArray().Length == 0) return;

                bool shouldMoveToEmpty = (bool) voidBlockInfos.First().Invoke(this, new object[] {direction});
                if (shouldMoveToEmpty) transform.position = transform.position.ToVector3Int() + direction;
                return;
            }

            var methodes = GetMethodsBySig(baseBlock.GetType(), true, typeof(DoInteractAttribute), typeof(void),
                GetType(), typeof(Vector3Int));

            var methodInfos = methodes as MethodInfo[] ?? methodes.ToArray();

            if (methodInfos.ToArray().Length == 0) return;

            methodInfos.First().Invoke(baseBlock, new object[] {this, direction});

            var myMethodes = GetMethodsBySig(GetType(), false, typeof(DidInteractAttribute), typeof(Boolean),
                baseBlock.GetType(), typeof(Vector3Int));

            var myMethodeInfo = methodes as MethodInfo[] ?? myMethodes.ToArray();
            if (myMethodeInfo.ToArray().Length == 0) return;

            bool shouldMove = (bool) myMethodeInfo.First().Invoke(this, new object[] {baseBlock, direction});
            if (shouldMove) transform.position = transform.position.ToVector3Int() + direction;
        }


        public BaseBlock GetBlockBeneath()
        {
            return GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + Vector3Int.down);
        }

        public BaseBlock GetBlockAbove()
        {
            return GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + Vector3Int.up);
        }

        private IEnumerable<MethodInfo> GetMethodsBySig(Type type, bool allowSubclass, Type attributeType,
            Type returnType, params Type[] parameterTypes)
        {
            return type.GetMethods().Where((m) =>
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
        }
    }
}