using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Utilities;

namespace Runtime.Blocks.Attributes
{
    public abstract class BlockAttribute : Attribute
    {
        public abstract bool IsValidSignature(MethodInfo methodInfo);
        
        /// <summary>
        /// Returns a IEnumerable with a list of methodes follow a specific signature within a class type.
        /// </summary>
        /// <param name="type">The type of the class</param>
        /// <param name="returnType">the return type of the methode</param>
        /// <param name="parameterTypes">the parameters the methode should support</param>
        /// <returns></returns>
        private IEnumerable GetMethodsBySig(Type type, Type returnType, params Type[] parameterTypes)
        {
            return type.GetMethods().Where((m) => IsValidSignature(m, returnType, parameterTypes));
        }

        /// <summary>
        /// Returns whether this MethodInfo follows a certain signature.
        /// </summary>
        /// <param name="m">The MethodeInfo object</param>
        /// <param name="returnType">The type this object should return</param>
        /// <param name="parameterTypes">The parameters this methode should follow.</param>
        /// <returns></returns>
        protected bool IsValidSignature(MethodInfo m, Type returnType, params Type[] parameterTypes)
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
        }
    }
}
