using System;
using UnityEngine;
using System.IO;
#if WX_GAME
using WeChatWASM;
#endif

namespace FrankGame
{
    public static class DataSaveManager
    {
        
        private static string DB_Local_Path
        {
            get
            {
#if UNITY_EDITOR
                return Application.dataPath.Replace("Assets", "") + "db/{0}.txt";
#elif WX_GAME
            return WX.env.USER_DATA_PATH + "/db/{0}.txt";
#else
            if (GameConfig.Package.ResLoadMode == FrankGame.ELoadMode.AssetBundle)
            {
                return Application.persistentDataPath + "/db/{0}.txt";
            }

            return Application.dataPath.Replace("Assets", "") + "/db/{0}.txt";
#endif
            }
        }

        public static void SaveData<T>(string _saveName, T _data, bool _isEncrypt, System.Action<bool> _onSaveDone = null) where T : class {
            var  _path = string.Format(DB_Local_Path, _saveName);
#if BYTE_GAME
            bool result = StarkSDKSpace.StarkSDK.API.Save(_data, _saveName);

            _onSaveDone?.Invoke(result);
#else
#if WX_GAME && !UNITY_EDITOR
            string dirPath = WX.env.USER_DATA_PATH + "/db";
#else
            string dirPath = Path.GetDirectoryName(_path);
#endif
            if (!FileSystemManager.IsDirectoryExists(dirPath))
            {
                FileSystemManager.CreateDirectory(dirPath);
            }

            string json;
            if (typeof(T) == typeof(string)) {
                json = _data as string;
            } else {
                json = JsonUtils.ToJson(_data);
            }
            if (_isEncrypt) {
                json = JsonEncryption.Encrypt(json);
            }

            FileSystemManager.WriteAllText(_path, json, (p) => 
            {
                _onSaveDone?.Invoke(true);
            }, 
            (p) => 
            {
                _onSaveDone?.Invoke(false);
            });
#endif
        }

        public static void RemoveData(string _saveName, System.Action<bool> _onSaveDone = null) {
            FileSystemManager.FileDelete(string.Format(DB_Local_Path, _saveName));
            _onSaveDone?.Invoke(true);
        }

        public static void LoadData(string _saveName, Type _dataType, bool _isEncrypt = false, System.Action<object> _onLoadDone = null) {
            LoadData<string>(_saveName, _isEncrypt, txt => {
                if (string.IsNullOrEmpty(txt)) {
                    _onLoadDone?.Invoke(default);
                    return;
                }

                //try {
                    _onLoadDone?.Invoke(JsonUtils.LoadObject<object>(txt, _dataType));
                //} catch (Exception ex) {
                //    Debug.LogError(ex);
                //    Debug.LogError($"读取数据文件[fileName={_saveName},dataType={_dataType.FullName}]失败：{ex.Message}");
                //}
            });
        }

        public static void LoadData<T>(string _saveName, bool _isEncrypt = false, System.Action<T> _onLoadDone = null)  where T : class
        {
            var  _path = string.Format(DB_Local_Path, _saveName);
#if BYTE_GAME
            T loaded = StarkSDKSpace.StarkSDK.API.LoadSaving<T>(typeof(T).Name);

            _onLoadDone?.Invoke(loaded);
#else
            if (!FileSystemManager.IsFileExists(_path))
            {
                _onLoadDone?.Invoke(default(T));
                return;
            }

            FileSystemManager.ReadAllText(_path, (json) =>
            {
                T jsonData;
                if (_isEncrypt) {
                    json = JsonEncryption.Decrypt(json);
                }
                if (typeof(T) == typeof(string)) {
                    jsonData = json as T;
                } else {
                    jsonData = JsonUtils.LoadObject<T>(json);
                }
                _onLoadDone?.Invoke(jsonData);
            });
#endif
        }


        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="_saveName"></param>
        /// <returns></returns>
        public static bool IsExists(string _saveName) {
            return FileSystemManager.IsFileExists(string.Format(DB_Local_Path, _saveName));
        }

#if UNITY_EDITOR
        public static void ClearAll() {
            var dirPath = Application.dataPath.Replace("Assets", "") + "db/";
            var dir = new DirectoryInfo(dirPath);
            var files = dir.GetFiles();
            foreach (var f in files) {
                f.Delete();
            }
            Debug.Log($"清除DB成功，共清除[{files.Length}]个文件。");
        }
#endif

        public static void ResetStartApp()
        {
#if BYTE_GAME
            StarkSDKSpace.StarkSDK.API.RestartApp();
#endif
        }
    }
}