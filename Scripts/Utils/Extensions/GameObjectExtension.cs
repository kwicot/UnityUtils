using System.Collections.Generic;
using Core.Scripts.Pool;
using UnityEngine;

namespace Kwicot.Core.Scripts.Utils.Extensions
{
    public static class GameObjectExtension
    {
        public static GameObject Disable(this GameObject gameObject)
        {
            if(gameObject != null)
                gameObject.SetActive(false);
            else
                Debug.LogWarning("Try disable null gameObject");
            
            return gameObject;
        }
        
        public static GameObject Enable(this GameObject gameObject)
        {
            if(gameObject != null)
                gameObject.SetActive(true);
            else
                Debug.LogWarning("Try enable null gameObject");
            
            return gameObject;
        }

        public static bool TryGetComponents<T>(this GameObject gameObject, out T[] components)
        {
            if (gameObject != null)
            {
                components = gameObject.GetComponentsInChildren<T>();
                return components.Length > 0;
            }
            else
            {
                Debug.LogWarning("Try get components from null gameObject");
                components = null;
                return false;
            }
        }

        public static GameObject Reuse(this GameObject gameObject, Vector3 position, Quaternion rotation)
        {
            if (gameObject != null)
            {
                var pool = PoolManager.Reuse(gameObject);
                if (pool != null)
                {
                    pool.transform.position = position;
                    pool.transform.rotation = rotation;
                    return pool;
                }
            }
            else
                Debug.Log("Try reuse null gameObject");

            return gameObject;
        }
        public static GameObject Reuse(this GameObject gameObject) => Reuse(gameObject, gameObject.transform.position, gameObject.transform.rotation);
        public static GameObject Reuse(this GameObject gameObject, Vector3 position) => Reuse(gameObject, position, gameObject.transform.rotation);

        public static void Populate(this GameObject gameObject, int count)
        {
            if(gameObject != null)
                PoolManager.Populate(gameObject, count);
            else
                Debug.LogWarning("Try populate null gameObject");
        }

        public static void Release(this GameObject gameObject)
        {
            if (gameObject != null)
                gameObject.Release();
            else
                Debug.LogWarning("Try release null gameObject");
        }
    }
}