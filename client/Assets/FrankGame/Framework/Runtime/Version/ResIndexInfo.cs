using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [Serializable]
    public class ResIndexInfo
    {
        public List<ResUpdateInfo> configList;//表格配置
        public List<ResUpdateInfo> resList;//资源列表
        public List<ResUpdateInfo> hotUpdateDllList;//热更代码资源列表
        public List<string> preloadHashList;//预加载的hash列表

        public void AddConfig(ResUpdateInfo _resUpdateInfo)
        {
            if (configList == null)
            {
                configList = new List<ResUpdateInfo>();
            }
            configList.Add(_resUpdateInfo);
        }

        public void AddRes(ResUpdateInfo _resUpdateInfo)
        {
            if (resList == null)
            {
                resList = new List<ResUpdateInfo>();
            }
            resList.Add(_resUpdateInfo);
        }

        public void AddDll(ResUpdateInfo _resUpdateInfo)
        {
            if (hotUpdateDllList == null)
            {
                hotUpdateDllList = new List<ResUpdateInfo>();
            }
            hotUpdateDllList.Add(_resUpdateInfo);
        }

        public void RemoveConfig(ResUpdateInfo _resUpdateInfo)
        {
            if (configList != null)
            {
                configList.Remove(_resUpdateInfo);
            }
        }

        public void RemoveRes(ResUpdateInfo _resUpdateInfo)
        {
            if (resList != null)
            {
                resList.Remove(_resUpdateInfo);
            }
        }

        public void RemoveDll(ResUpdateInfo _resUpdateInfo)
        {
            if (hotUpdateDllList != null)
            {
                hotUpdateDllList.Remove(_resUpdateInfo);
            }
        }

        public void Clone(ResIndexInfo _resIndexInfo)
        {
            this.configList = new List<ResUpdateInfo>();
            if (_resIndexInfo.configList != null)
            {
                foreach (var updateInfo in _resIndexInfo.configList)
                {
                    ResUpdateInfo info = new ResUpdateInfo();
                    info.Clone(updateInfo);

                    this.configList.Add(info);
                }
            }

            this.resList = new List<ResUpdateInfo>();
            if (_resIndexInfo.resList != null)
            {
                foreach (var updateInfo in _resIndexInfo.resList)
                {
                    ResUpdateInfo info = new ResUpdateInfo();
                    info.Clone(updateInfo);

                    this.resList.Add(info);
                }
            }

            this.hotUpdateDllList = new List<ResUpdateInfo>();
            if (_resIndexInfo.hotUpdateDllList != null)
            {
                foreach (var updateInfo in _resIndexInfo.hotUpdateDllList)
                {
                    ResUpdateInfo info = new ResUpdateInfo();
                    info.Clone(updateInfo);

                    this.hotUpdateDllList.Add(info);
                }
            }

            this.preloadHashList = new List<string>(_resIndexInfo.preloadHashList);
        }

        public void Merge(ResIndexInfo _resIndexInfo)
        {
            if (this.configList == null)
            {
                this.configList = new List<ResUpdateInfo>();
            }
            
            if (_resIndexInfo.configList != null)
            {
                foreach (var updateInfo in _resIndexInfo.configList)
                {
                    ResUpdateInfo info = new ResUpdateInfo();
                    info.Clone(updateInfo);

                    this.configList.Add(info);
                }
            }

            if (this.resList == null)
            {
                this.resList = new List<ResUpdateInfo>();
            }
            
            if (_resIndexInfo.resList != null)
            {
                foreach (var updateInfo in _resIndexInfo.resList)
                {
                    ResUpdateInfo info = new ResUpdateInfo();
                    info.Clone(updateInfo);

                    this.resList.Add(info);
                }
            }

            if (this.hotUpdateDllList == null)
            {
                this.hotUpdateDllList = new List<ResUpdateInfo>();
            }
            
            if (_resIndexInfo.hotUpdateDllList != null)
            {
                foreach (var updateInfo in _resIndexInfo.hotUpdateDllList)
                {
                    ResUpdateInfo info = new ResUpdateInfo();
                    info.Clone(updateInfo);

                    this.hotUpdateDllList.Add(info);
                }
            }

            if (this.preloadHashList == null)
            {
                this.preloadHashList = new List<string>();
            }

            if (_resIndexInfo.preloadHashList != null)
            {
                this.preloadHashList.AddRange(_resIndexInfo.preloadHashList);
            }
        }

        public void Read(BinaryReader _binaryReader)
        {
            int configCount = _binaryReader.ReadInt32();
            this.configList = new List<ResUpdateInfo>();
            for (int i = 0; i < configCount; i++)
            {
                ResUpdateInfo updateInfo = new ResUpdateInfo();
                updateInfo.Read(_binaryReader);

                this.configList.Add(updateInfo);
            }

            int dListCount = _binaryReader.ReadInt32();
            this.resList = new List<ResUpdateInfo>();
            for (int i = 0; i < dListCount; i++)
            {
                ResUpdateInfo dependInfo = new ResUpdateInfo();
                dependInfo.Read(_binaryReader);

                this.resList.Add(dependInfo);
            }

            int dllCount = _binaryReader.ReadInt32();
            this.hotUpdateDllList = new List<ResUpdateInfo>();
            for (int i = 0; i < dllCount; i++)
            {
                ResUpdateInfo updateInfo = new ResUpdateInfo();
                updateInfo.Read(_binaryReader);

                this.hotUpdateDllList.Add(updateInfo);
            }

            int preloadListCount = _binaryReader.ReadInt32();
            this.preloadHashList = new List<string>();
            for (int i = 0; i < preloadListCount; i++)
            {
                this.preloadHashList.Add(_binaryReader.ReadString());
            }
        }

        public void Write(BinaryWriter _binaryWriter)
        {
            if (this.configList != null)
            {
                _binaryWriter.Write(this.configList.Count);
                for (int i = 0; i < this.configList.Count; i++)
                {
                    this.configList[i].Write(_binaryWriter);
                }
            }
            else
            {
                _binaryWriter.Write(0);
            }

            if (this.resList != null)
            {
                _binaryWriter.Write(this.resList.Count);
                for (int i = 0; i < this.resList.Count; i++)
                {
                    this.resList[i].Write(_binaryWriter);
                }
            }
            else
            {
                _binaryWriter.Write(0);
            }

            if (this.hotUpdateDllList != null)
            {
                _binaryWriter.Write(this.hotUpdateDllList.Count);
                for (int i = 0; i < this.hotUpdateDllList.Count; i++)
                {
                    this.hotUpdateDllList[i].Write(_binaryWriter);
                }
            }
            else
            {
                _binaryWriter.Write(0);
            }

            if (this.preloadHashList != null)
            {
                _binaryWriter.Write(this.preloadHashList.Count);
                for (int i = 0; i < this.preloadHashList.Count; i++)
                {
                    _binaryWriter.Write(this.preloadHashList[i]);
                }
            }
            else
            {
                _binaryWriter.Write(0);
            }
            
        }
    }
}
