using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [Serializable]
    public class ResUpdateInfo
    {
        public string localPath;
        public string hash;
        public string name;
        public long size;
        public List<string> dependList;

        public void Clone(ResUpdateInfo _updateInfo)
        {
            this.localPath = _updateInfo.localPath;
            this.hash = _updateInfo.hash;
            this.name = _updateInfo.name;
            this.size = _updateInfo.size;
            if (_updateInfo.dependList != null)
            {
                this.dependList = new List<string>(_updateInfo.dependList);
            }
        }

        public void Read(BinaryReader _binaryReader)
        {
            this.localPath = _binaryReader.ReadString();
            this.hash = _binaryReader.ReadString();
            this.name = _binaryReader.ReadString();
            this.size = _binaryReader.ReadInt64();

            int dependenciesCount = _binaryReader.ReadInt32();
            this.dependList = new List<string>();
            for (int i = 0; i < dependenciesCount; i++)
            {
                this.dependList.Add(_binaryReader.ReadString());
            }
        }

        public void Write(BinaryWriter _binaryWriter)
        {
            _binaryWriter.Write(this.localPath);
            _binaryWriter.Write(this.hash);
            _binaryWriter.Write(this.name);
            _binaryWriter.Write(this.size);

            if (this.dependList != null)
            {
                _binaryWriter.Write(this.dependList.Count);
                for (int i = 0; i < this.dependList.Count; i++)
                {
                    _binaryWriter.Write(this.dependList[i]);
                }
            }
            else
            {
                _binaryWriter.Write(0);
            }
        }
    }
}
