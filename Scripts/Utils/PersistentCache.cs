using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace Kwicot.Core.Scripts.Utils
{
    public static class PersistentCache
    {
        private static bool _initialized = false;
        private static bool _isLog;
        
        private static void Initialize()
        {
        }
        public static void Save(object data) => Save(data, data.GetType().ToString());
        public static void Save(object data, string key)
        {
            if (!_initialized)
                Initialize();
            
            if(data == null)
                return;

            var path = GetPath(key);
                
            if(File.Exists(path))
                File.Delete(path);
            
            var bf = new BinaryFormatter();
            var fs = new FileStream(path, FileMode.OpenOrCreate);
            
            try
            {
                bf.Serialize(fs, data);
                
                fs.Close();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                fs.Close();
            }
        }
        public static T TryLoad<T>(string key)
        {
            if(!_initialized)
                Initialize();
            
            var path = GetPath(key);
            if (File.Exists(path))
            {
                var bf = new BinaryFormatter();
                var fs = new FileStream(path, FileMode.Open);
                try
                {
                    var data = bf.Deserialize(fs);
                    fs.Close();
                    return (T)data;
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    fs.Close();
                    return default;
                }
                
            }
            else
            {
                Debug.LogError($"Try to load {path} but doesnt exists");
                return default;
            }
        }
        public static T TryLoad<T>() => TryLoad<T>(typeof(T).ToString());

        public static void TryDelete(string key)
        {
            if(!_initialized)
                Initialize();
            
            var path = GetPath(key);
            if(File.Exists(path))
                File.Delete(path);
        }
        public static void TryDelete(params string[] keys)
        {
            if(!_initialized)
                Initialize();
            
            foreach (var key in keys)
            {
               TryDelete(key);
            }
        }
        static string GetPath(string key)
        {
            if(!_initialized)
                Initialize();
            
            var path = Application.persistentDataPath;
            return Path.Combine(path, key);
        }

        static void Log(object data)
        {
            if(_isLog)
                Debug.Log(data);
        }
        static void LogWarning(object data)
        {
            if(_isLog)
                Debug.LogWarning(data);
        }

        static void LogError(object data)
        {
            if(_isLog)
                Debug.LogError(data);
        }

#if UNITY_EDITOR
        [MenuItem("Tools/PersistentCache/Clear persistent cache")]
#endif

        public static void ClearPersistentCache()
        {
            if(!_initialized)
                Initialize();
            
            var files = Directory.GetFiles(Application.persistentDataPath);
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
#if UNITY_EDITOR
        [MenuItem("Tools/PersistentCache/Clear player prefs cache")]
#endif
        public static void ClearPrefsCache()
        {
            PlayerPrefs.DeleteAll();
        }
        
#if UNITY_EDITOR
        [MenuItem("Tools/PersistentCache/Clear all cache")]
#endif

        public static void CelarAllCache()
        {
            ClearPersistentCache();
            ClearPrefsCache();
        }
        
    }
}