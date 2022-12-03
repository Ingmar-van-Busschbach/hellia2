using System;
using UnityEngine;

namespace Utilities
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// Stores the object as a location private variable.
        /// </summary>
        private static T _instance;

        /// <summary>
        /// The public access point for the singleton.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null) _instance = CreateSingleton();
                return _instance;
            }
        }

        /// <summary>
        /// Creates the singleton object
        /// </summary>
        /// <returns></returns>
        private static T CreateSingleton()
        {
            T ownerObject = FindObjectOfType<T>();
            if (ownerObject != null) return ownerObject;

            var gameObject = new GameObject($"{typeof(T).Name} (singleton)");
            ownerObject = gameObject.AddComponent<T>();
            DontDestroyOnLoad(ownerObject);
            return ownerObject;
        }
    }
}