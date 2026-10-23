using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
#if WX_GAME
using WeChatWASM;
#endif

namespace FrankGame
{
    public static class FileSystemManager
    {
        public static bool IsFileExists(string _path)
        {
#if BYTE_GAME
            return StarkSDKSpace.StarkSDK.API.GetStarkFileSystemManager().AccessSync(_path);
#elif WX_GAME
            string result = WX.GetFileSystemManager().AccessSync(_path);
            return result == "access:ok";
#else   
            return File.Exists(_path);
#endif
        }

        public static bool IsDirectoryExists(string _path)
        {
#if BYTE_GAME
            return StarkSDKSpace.StarkSDK.API.GetStarkFileSystemManager().AccessSync(_path);
#elif WX_GAME
            string result = WX.GetFileSystemManager().AccessSync(_path);
            return result == "access:ok";
#else
            return Directory.Exists(_path);
#endif
        }

        public static void CreateDirectory(string _path)
        {
#if BYTE_GAME
            StarkSDKSpace.StarkSDK.API.GetStarkFileSystemManager().MkdirSync(_path);
#elif WX_GAME
            WX.GetFileSystemManager().MkdirSync(_path, true);
#else
            Directory.CreateDirectory(_path);
#endif
        }

        public static void ReadAllText(string _path, Action<string> _onReadText)
        {
#if BYTE_GAME
            var readFileParam = new StarkSDKSpace.ReadFileParam();
            readFileParam.filePath = _path;
            readFileParam.success = (response) =>
            {
                _onReadText?.Invoke(response.stringData);
            };
            readFileParam.fail = (response) =>
            {
                Log.Error("文件读取失败：" + _path);
            };

            StarkSDKSpace.StarkSDK.API.GetStarkFileSystemManager().ReadFile(readFileParam);
#elif WX_GAME
            var readFileParam = new ReadFileParam();
            readFileParam.filePath = _path;
            readFileParam.encoding = "utf8";
            readFileParam.success = (succ) =>
            {
                _onReadText?.Invoke(succ.stringData);
            };
            readFileParam.fail = (fail) =>
            {
                _onReadText?.Invoke(null);
            };

            WX.GetFileSystemManager().ReadFile(readFileParam);
#else
            string text = File.ReadAllText(_path);

            _onReadText?.Invoke(text);
#endif
        }

        public static void WriteAllText(string _path, string _text, Action<string> _onWriteSuccess, Action<string> _onWriteFail = null)
        {
#if BYTE_GAME
            var writeFileParam = new StarkSDKSpace.WriteFileStringParam();
            writeFileParam.filePath = _path;
            writeFileParam.data = _text;
            writeFileParam.success = (response) =>
            {
                _onWriteSuccess?.Invoke(_path);
            };
            writeFileParam.fail = (response) =>
            {
                Log.Error("文件写入失败：" + _path);

                _onWriteFail?.Invoke(_path);
            };

            StarkSDKSpace.StarkSDK.API.GetStarkFileSystemManager().WriteFile(writeFileParam);
#elif WX_GAME
            var writeFileParam = new WriteFileStringParam();
            writeFileParam.filePath = _path;
            writeFileParam.encoding = "utf8";
            writeFileParam.data = _text;
            writeFileParam.success = (succ) =>
            {
                _onWriteSuccess?.Invoke(_path);
            };
            writeFileParam.fail = (fail) =>
            {
                _onWriteFail?.Invoke(_path);
            };

            WX.GetFileSystemManager().WriteFile(writeFileParam);
#else
            File.WriteAllText(_path, _text);

            _onWriteSuccess?.Invoke(_path);
#endif
        }

        public static void FileDelete(string _path)
        {
#if BYTE_GAME
            StarkSDKSpace.StarkSDK.API.GetStarkFileSystemManager().UnlinkSync(_path);
#elif WX_GAME
            WX.GetFileSystemManager().RmdirSync(_path, false);
#else
            File.Delete(_path);
#endif
        }
    }
}