using System;
using System.Collections.Generic;
using Kwicot.Core.Scripts.Utils.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Scripts.Pool
{
    public static class PoolManager
    {
        static Dictionary<GameObject, List<GameObject>> _poolMap = new Dictionary<GameObject, List<GameObject>>();
        
        public static void Populate(GameObject prefab, int count)
        {
            var list = new List<GameObject>();
            for (int i = 0; i < count; i++)
            {
                var obj = Object.Instantiate(prefab);
                obj.name = $"{prefab.name}_Pool_{i}";
                obj.Disable();
                list.Add(obj);
            }
            
            if(!_poolMap.TryAdd(prefab, list))
                _poolMap[prefab].AddRange(list);
        }

        public static GameObject Reuse(GameObject prefab)
        {
            if (!_poolMap.TryGetValue(prefab, out var list))
            {
                foreach (var gameObject in list)
                {
                    if (gameObject.activeSelf == false)
                    {
                        if (gameObject.TryGetComponents(out IPooledObject[] pooledObjects))
                        {
                            foreach (var pooledObject in pooledObjects)
                                pooledObject.OnReuse();
                        }
                        
                        return gameObject;
                    }
                }
                throw new IndexOutOfRangeException("There is no free pool for this prefab");
            }
            throw new NullReferenceException("There is no pool for this prefab");
        }

        public static void Release(GameObject gameObject)
        {
            if (gameObject.TryGetComponents(out IPooledObject[] pooledObjects))
            {
                foreach (var pooledObject in pooledObjects)
                    pooledObject.OnRelease();
            }
        }
    }
}