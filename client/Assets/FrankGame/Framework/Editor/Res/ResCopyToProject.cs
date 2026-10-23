using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

namespace FrankGame
{
    
public static class ResCopyToProject {
    private class FileMd5
    {
        public FileInfo fileInfo;
        public string md5;
    }

    private static readonly string Art_Root_Path = Path.Combine(Application.dataPath, "../../art/");

    private static readonly string Pro_Root_Path = Path.Combine(GameConfig.Path.LocalResRoot, "arts");


    private static readonly string[] CopyDirs = { "Font", "UI", "Map", "Device", "Effect", "Audio"};
    

    private static readonly string[] CopyExtList = { ".png" , ".fnt", ".jpg", ".ttf", ".txt", ".bytes", ".mp3"};


    private static readonly Dictionary<string, string> Md5Files = new();


    private const string Spine_Suffix = ".atlas.txt";

    private static readonly string[] SpineAutoSuffix = { "_Atlas.asset", "_Material.mat", "_SkeletonData.asset", "_Material-Multiply.mat" };

    [MenuItem("FrankGame/Tools/导入素材到工程", false)]
    public static void CopyAllRes() {
        Md5Files.Clear();
        ResCopyUtils.Iterator(CopyDirs, o => CopyDir(new DirectoryInfo(Art_Root_Path + Path.DirectorySeparatorChar + o), new DirectoryInfo(Pro_Root_Path + Path.DirectorySeparatorChar + o)));
    }

    /// <summary>
    /// 复制
    /// </summary>
    /// <param name="_artDir"></param>
    /// <param name="_proDir"></param>
    private static void CopyDir(DirectoryInfo _artDir, DirectoryInfo _proDir) {
        List<string> removeFileList = new(64);
        Dictionary<string, string> copyFileDic = new(64);
        CheckCopyDir(_artDir, _proDir, removeFileList, copyFileDic);
        foreach (var dic in copyFileDic) {
            FileUtils.CopyFile(dic.Key, dic.Value);
            Debug.Log("Copy to: " + dic.Value);
        }
        foreach (var file in removeFileList) {
            File.Delete(file);
            Debug.Log("Delete: " + file);
        }
        var copyCounter = copyFileDic.Count;
        var removeCounter = removeFileList.Count;
        Debug.Log($"目录[{_artDir.FullName}]复制图片数：{copyCounter}, 删除图片数：{removeCounter}");
        AssetDatabase.Refresh();
    }

    private static void CheckCopyDir(DirectoryInfo _artDir, DirectoryInfo _proDir, List<string> removeFileList, Dictionary<string, string> copyFileDic) {
       
        var existDir = _proDir.Exists;
        if (!existDir) {
            _proDir.Create();
        }
        // 检查 源是否存在，如果源不存在，则目标目录直接删除
        if (_artDir is not { Exists: true }) {
            ResCopyUtils.Iterator(_proDir.GetFiles(), f => removeFileList.Add(f.FullName), f => f.Extension != Meta_Ext);
           // 处理 目录
           ResCopyUtils.Iterator(_proDir.GetDirectories(), d => CheckCopyDir(null, d, removeFileList, copyFileDic)); 
           return;
        }
        
        // 处理 art dir 子目录
        ResCopyUtils.Iterator(_artDir.GetDirectories(), f => CheckCopyDir(f, new DirectoryInfo(_proDir.FullName + Path.DirectorySeparatorChar + f.Name), removeFileList, copyFileDic));

        var sourceExistFiles = _artDir.GetFiles();
        var targetExistFiles = _proDir.GetFiles();
        
        var sourceFileMd5Dic = new Dictionary<string, FileMd5>();
        var targetFileMd5Dic = new Dictionary<string, FileMd5>();
        foreach (var file in sourceExistFiles) {
            if (!CopyExtList.Contains(file.Extension.ToLower())) {
                continue;
            }
            var fileMd5 = new FileMd5();
            fileMd5.fileInfo = file;
            fileMd5.md5 = ResCopyUtils.CreateMD5(file.FullName);
            // 检查文件是否重复
            if (Md5Files.TryGetValue(fileMd5.md5, out var md5File)) {
                Debug.LogWarning($"文件[{file.Name}]与[{md5File}]文件内容重复");
            } else {
                Md5Files.Add(fileMd5.md5, fileMd5.fileInfo.Name);
            }
            sourceFileMd5Dic.Add(file.Name, fileMd5);
        }

        foreach (var file in targetExistFiles) {
            if (file.Extension == Meta_Ext) {
                continue;
            }
            FileMd5 fileMd5 = new FileMd5();
            fileMd5.fileInfo = file;
            fileMd5.md5 = ResCopyUtils.CreateMD5(file.FullName);
            targetFileMd5Dic.Add(file.Name, fileMd5);
        }

        foreach (var dic in sourceFileMd5Dic) {
            var sourceFile = dic.Key;
            var sourceMd5 = dic.Value;
            if (existDir) {
                if (targetFileMd5Dic.TryGetValue(sourceFile, out FileMd5 targetMd5)) {
                    //md5不等需要重新拷贝
                    if (!sourceMd5.md5.Equals(targetMd5.md5)) {
                        copyFileDic.Add(sourceMd5.fileInfo.FullName, targetMd5.fileInfo.FullName);
                    }
                    
                    targetFileMd5Dic.Remove(sourceFile);
                }
                else {
                    copyFileDic.Add(sourceMd5.fileInfo.FullName, _proDir.FullName + "/" + sourceMd5.fileInfo.Name);
                }
            }
            else {
                copyFileDic.Add(sourceMd5.fileInfo.FullName, _proDir.FullName + "/" + sourceMd5.fileInfo.Name);
            }
        }

        if (targetFileMd5Dic.Count > 0)
        {
            foreach (var dic in targetFileMd5Dic) {
                if (IsDelete(dic.Value.fileInfo, sourceExistFiles)) {
                    removeFileList.Add(dic.Value.fileInfo.FullName);
                }
            }
        }
    }

    private static bool IsDelete(FileInfo file, FileInfo[] sourceExistFiles) {
        foreach (var sf in SpineAutoSuffix) {
            if (file.Name.EndsWith(sf)) {
                return !HasSpineSource(file.Name.Substring(0, file.Name.Length - sf.Length), sourceExistFiles);
            }
        }
        return true;
    }

    private static bool HasSpineSource(string name, FileInfo[] sourceExistFiles) {
        foreach (var ff in sourceExistFiles) {
            if (ff.Name.Equals($"{name}{Spine_Suffix}")) {
                return true;
            }
        }
        return false;
    }

    private const string Meta_Ext = ".meta";
}

}