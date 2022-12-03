using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Runtime.Blocks.Attributes
{
    [MeansImplicitUse]
    public abstract class BlockAttribute : Attribute
    {
        /// <summary>
        /// Abstract method to check if then given MethodInfo follows the required signature for this attribute
        /// </summary>
        /// <param name="methodInfo">The methodInfo we wish to check</param>
        /// <returns>true is this method is setup correctly.</returns>
        public abstract bool IsValidSignature(MethodInfo methodInfo);
        
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
