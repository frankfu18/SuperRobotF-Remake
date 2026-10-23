using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class HotUpdateSystem : SystemBase<HotUpdateSystem>
{
    private static List<string> Hot_Dll_Name_List = new List<string>()
    {
        "Perfect",
        "HotUpdate",
    };

    private Assembly hotUpdateAssembly;
    private Assembly prefectAssembly;

    public void LoadHotUpdateAssembly()
    {
#if ENABLE_HOTUPDATE
        FrankGame.Log.Debug("加载HotUpdateAssembly");

        //补充AOT元数据
        LoadMetadataForAOTAssembly();

        prefectAssembly = LoadAssembly(Hot_Dll_Name_List[0]);
        hotUpdateAssembly = LoadAssembly(Hot_Dll_Name_List[1]);

        hotUpdateAssembly.GetType("HotUpdateMain").GetMethod("Run").Invoke(null, null);
#else
        hotUpdateAssembly = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == Hot_Dll_Name_List[1]);
        if (hotUpdateAssembly != null)
        {
            hotUpdateAssembly.GetType("HotUpdateMain").GetMethod("Run").Invoke(null, null);
        }
#endif
    }

    public void Login()
    {
        hotUpdateAssembly.GetType("HotUpdateMain").GetMethod("Login").Invoke(null, null);
    }

    public void Logout()
    {
        hotUpdateAssembly.GetType("HotUpdateMain").GetMethod("Logout").Invoke(null, null);
    }

#if ENABLE_HOTUPDATE
    private Assembly LoadAssembly(string _assemblyName)
    {
        Assembly assembly = null;
#if UNITY_EDITOR
        assembly = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == _assemblyName);
#else
        string dllPath = FrankGame.PathUtil.GetLocalDllPath() + FrankGame.BundleManager.Instance.GetDllHash(_assemblyName);
        assembly = Assembly.Load(System.IO.File.ReadAllBytes(dllPath));
#endif

        return assembly;
    }

    private void LoadMetadataForAOTAssembly()
    {
        Dictionary<string, string> dllNameDic = FrankGame.BundleManager.Instance.GetDllNameHashDic();

        foreach (var dic in dllNameDic)
        {
            string dllName = dic.Key;
            if (Hot_Dll_Name_List.Contains(dllName))
            {
                continue;
            }

            string dllPath = FrankGame.PathUtil.GetLocalDllPath() + dic.Value;
            byte[] bytes = System.IO.File.ReadAllBytes(dllPath);

            HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(bytes, HybridCLR.HomologousImageMode.SuperSet);
        }
    }
#endif
}
