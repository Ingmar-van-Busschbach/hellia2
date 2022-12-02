using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public abstract BlockType BlockType { get; }

        protected bool CanMove(Vector3Int direction)
        {
            if (direction == Vector3Int.zero) return false;
            BaseBlock baseBlock = GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + direction);
            if (baseBlock == null) return false;

            var methodes = GetMethodsBySig(baseBlock.GetType(), typeof(Boolean), this.GetType(), typeof(Vector3Int));
            if (methodes == null || methodes.ToArray().Length == 0) return false;

            bool result = (bool)methodes.First().Invoke(baseBlock, new object[] { this, direction });
            return false;
        }

        protected void DoMove(Vector3Int direction)
        {
            if (!CanMove(direction)) Debug.LogWarning("[BaseBlock] Blocked move but it should be able to");

            transform.position += direction;
        }

        /// <summary>
        /// Try overtake the block at the specific position. 
        /// </summary>
        /// <param name="newPosition">The position we try to overtake</param>
        public abstract bool TryOverTake(Vector3Int newPosition);

        /// <summary>
        /// Called when a block tries to overtake you. 
        /// </summary>
        /// <param name="baseBlock">The block trying to overtake you</param>
        /// <param name="direction">The block's moving direction</param>
        public abstract bool CanBeTakenOverBy(BaseBlock baseBlock, Vector3Int direction);

        /// <summary>
        /// When a block tries to overtake you, this function is called
        /// </summary>
        /// <param name="baseBlock">The block overtaking you</param>
        /// <param name="direction">The direction the block is moving</param>
        /// <returns></returns>
        public abstract bool OnGettingTakenOver(BaseBlock baseBlock, Vector3Int direction);

        public BaseBlock GetBlockBeneath()
        {
            return GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + Vector3Int.down);
        }

        public BaseBlock GetBlockAbove()
        {
            return GridManager.Instance.GetBlockAt(transform.position.ToVector3Int() + Vector3Int.up);
        }

        private IEnumerable<MethodInfo> GetMethodsBySig(Type type, Type returnType, params Type[] parameterTypes)
        {
            return type.GetMethods().Where((m) =>
            {
                if (m.ReturnType != returnType) return false;
                var parameters = m.GetParameters();
                
                if ((parameterTypes == null || parameterTypes.Length == 0)) return parameters.Length == 0;
                
                if (parameters.Length != parameterTypes.Length) return false;
                
                for (var i = parameterTypes.Length - 1; i >= 0; i--)
                {
                    if (parameters[i].ParameterType != parameterTypes[i]) return false;
                }

                return true;
            });
        }
    }
}