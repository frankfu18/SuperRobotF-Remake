using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileUtils
{
    //清除文本内容
    public static void CleanFileContent(string filePath)
    {
        FileStream fs = new FileStream(filePath, FileMode.Truncate, FileAccess.ReadWrite);
        fs.Close();
    }

    /// <summary>
    /// 复制文件夹内容到新的文件夹
    /// </summary>
    /// <param name="originPath"></param>
    /// <param name="targetPath"></param>
    public static void CopyDirectory(string originPath, string targetPath, Func<FileSystemInfo, bool> _filter = null)
    {
        try
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            DirectoryInfo dir = new DirectoryInfo(originPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)
                {     //判断是否文件夹
                    if (!Directory.Exists(targetPath + "/" + i.Name))
                    {
                        Directory.CreateDirectory(targetPath + "/" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                    }
                    CopyDirectory(i.FullName, targetPath + "/" + i.Name);    //递归调用复制子文件夹
                }
                else
                {
                    if (_filter == null || _filter(i))
                    {
                        File.Copy(i.FullName, targetPath + "/" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public static List<string> FindFilesInDirectoryByFilter(string _dirPath, System.Func<string, bool> _filter = null)
    {
        List<string> files = new List<string>();
        if (!Directory.Exists(_dirPath))
        {
            return files;
        }

        DirectoryInfo dirInfo = new DirectoryInfo(_dirPath);
        foreach (FileInfo file in dirInfo.GetFiles())
        {
            if (_filter == null || _filter(file.FullName))
            {
                files.Add(file.FullName);
            }
        }

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
        {
            files.AddRange(FindFilesInDirectoryByFilter(dir.FullName, _filter));
        }

        return files;
    }

    public static void FindFilesInDirectoryByFilter(string _dirPath, System.Action<FileInfo> _filter = null)
    {
        if (!Directory.Exists(_dirPath))
        {
            return;
        }

        DirectoryInfo dirInfo = new DirectoryInfo(_dirPath);
        foreach (FileInfo file in dirInfo.GetFiles())
        {
            _filter?.Invoke(file);
        }

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
        {
            FindFilesInDirectoryByFilter(dir.FullName, _filter);
        }
    }

    public static void DeleteFilesInDirectoryByFilter(string _dirPath, System.Func<string, bool> _filter = null)
    {
        List<string> files = FindFilesInDirectoryByFilter(_dirPath, _filter);
        foreach (string fileName in files)
        {
            File.Delete(fileName);
        }
    }

    public static void ClearDirectory(string _dirPath)
    {
        if (Directory.Exists(_dirPath))
        {
            Directory.Delete(_dirPath, true);
        }
        Directory.CreateDirectory(_dirPath);
    }

    public static void MoveFile(string oldFilePath, string newFilePath, bool overwrite = true)
    {
        if (!File.Exists(oldFilePath) || oldFilePath == newFilePath)
            return;
        string s = Path.GetDirectoryName(newFilePath);
        if (!Directory.Exists(s))
        {
            Directory.CreateDirectory(s);
        }
        File.Copy(oldFilePath, newFilePath, overwrite);
        File.Delete(oldFilePath);
    }

    public static void CopyFile(string oldFilePath, string newFilePath, bool overwrite = true)
    {
        if (!File.Exists(oldFilePath) || oldFilePath == newFilePath)
            return;
        string s = Path.GetDirectoryName(newFilePath);
        if (!Directory.Exists(s))
        {
            Directory.CreateDirectory(s);
        }
        File.Copy(oldFilePath, newFilePath, overwrite);
    }

    public static void FileEncrypt(string filePath, byte[] key)
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        using (var fs = File.OpenWrite(filePath))
        {
            fs.Write(key, 0, key.Length);
            fs.Close();
        }
    }

    public static void ReadBytes(byte[] _bytes, Action<BinaryReader> _onRead)
    {
        if (_bytes == null)
        {
            return;
        }

        using (MemoryStream ms = new MemoryStream(_bytes))
        {
            using (BinaryReader br = new BinaryReader(ms))
            {
                _onRead?.Invoke(br);

                br.Close();
                br.Dispose();
            }

            ms.Close();
            ms.Dispose();
        }
    }

    public static void ReadBytes(string _filePath, Action<BinaryReader> _onRead)
    {
        ReadBytes(File.ReadAllBytes(_filePath), _onRead);
    }

    public static byte[] WriteBytes(Action<BinaryWriter> _onWrite)
    {
        MemoryStream memoryStream = new MemoryStream();

        using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
        {
            _onWrite?.Invoke(binaryWriter);

            binaryWriter.Close();
            binaryWriter.Dispose();
        }

        byte[] _bytes = memoryStream.ToArray();

        return _bytes;
    }

    public static void WriteBytes(string _filePath, Action<BinaryWriter> _onWrite)
    {
        byte[] bytes = WriteBytes(_onWrite);

        if (bytes == null)
        {
            return;
        }

        File.WriteAllBytes(_filePath, bytes);
    }

    public static string AssetPathToFilePath(string _assetPath)
    {
        string filePath = Application.dataPath.Replace("Assets", "");
        filePath += _assetPath;

        return filePath;
    }

    public static string FilePathToAssetPath(string _filePath)
    {
        string assetPath = _filePath;
        int assetsStartIdx = assetPath.IndexOf("Assets");
        assetPath = assetPath.Substring(assetsStartIdx);

        return assetPath;
    }
}

