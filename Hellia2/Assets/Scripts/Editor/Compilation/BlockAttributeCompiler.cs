using System;
using System.Linq;
using System.Reflection;
using Runtime.Blocks.Attributes;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace Editor.Compilation
{
    public class BlockAttributeCompiler : UnityEditor.Editor
    {
        /// <summary>
        /// Subscribe to the assemblyCompilationFinished so we can do custom errors after compiling.
        /// we use the Finished event as we only want the latest compiled data.
        /// </summary>
        [InitializeOnLoadMethod]
        public static void AddEvent()
        {
            CompilationPipeline.assemblyCompilationFinished += CheckAttributeSignatures;
        }

        private void OnDestroy()
        {
            CompilationPipeline.assemblyCompilationFinished -= CheckAttributeSignatures;
        }


        /// <summary>
        /// Finds all instances of the BlockAttribute and checks whether the methode signatures are valid.
        /// if not it will print an error to the console so we are notified of possible bugs within our code base
        /// </summary>
        /// <param name="arg1">assembly compilation arugment</param>
        /// <param name="arg2">compiler messages arror</param>
        private static void CheckAttributeSignatures(string arg1, CompilerMessage[] arg2)
        {
            var extractedMethodes = TypeCache.GetMethodsWithAttribute<BlockAttribute>();
        
            foreach (var extractedMethode in extractedMethodes)
            {
                bool isValid = CheckAttributeSignature(extractedMethode);
            
                if (!isValid)
                {
                    Debug.LogWarning($"Invalid method ${extractedMethode}");
                }
            }
        }

        private static bool CheckAttributeSignature(MethodInfo methodInfo)
        {
            var attribute = methodInfo.CustomAttributes.First(data => data.AttributeType.BaseType == typeof(BlockAttribute));
            var instance = Activator.CreateInstance(attribute.AttributeType) as BlockAttribute;
            if (instance == null) return false;
            return instance.IsValidSignature(methodInfo);
        }
    }
}