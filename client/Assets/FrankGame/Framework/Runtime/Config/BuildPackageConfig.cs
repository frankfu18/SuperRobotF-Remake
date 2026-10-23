using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/BuildPackageConfig", fileName = "BuildPackageConfig")]
    public class BuildPackageConfig : SingleScriptableObject<BuildPackageConfig>
    {
        [Header("预加载资源包名称(不带后缀)列表")]
        public List<string> PreloadBundleList;//预加载列表，wx时在loading时加载，需要填资源包的名字不带后缀
    }
}