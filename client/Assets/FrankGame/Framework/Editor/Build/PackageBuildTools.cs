using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;

namespace FrankGame
{
    public class PackageBuildTools
    {
        [MenuItem("FrankGame/打包/生成版本索引文件")]
        public static void BuildVersionIndexFile()
        {
            VersionIndexInfo versionIndexInfo = new VersionIndexInfo();
            versionIndexInfo.new_client_version = "1.0.0";
            versionIndexInfo.serverList_url = "";
            versionIndexInfo.announcement_url = "";
            versionIndexInfo.client_version_list = new List<ClientVersionInfo>();

            ClientVersionInfo verInfo = new ClientVersionInfo();
            verInfo.version = "1.0.0";
            verInfo.debug = true;
            verInfo.res_url = "";
            verInfo.files_url = "";

            versionIndexInfo.client_version_list.Add(verInfo);

            string jsonContent = JsonUtility.ToJson(versionIndexInfo, true);

            File.WriteAllText(Application.dataPath + "/VersionIndex.json", jsonContent);

            JsonUtility.FromJson<VersionIndexInfo>(File.ReadAllText(Application.dataPath + "/VersionIndex.json"));

            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Create/FrankGame/XML/PackageLocalizationTable")]
        public static void CreatePackageLocalizationConfig()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            if (path.Contains("."))//说明是文件
            {
                Debug.LogError("请选择文件夹!");
                return;
            }

            Debug.Log("path =" + path);

            //创建xml对象
            XmlDocument xml = new XmlDocument();
            //创建声明
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "UTF-8", null));
            //创建根节点
            XmlNode root = xml.CreateElement("PackageLocalization");
            //设置根节点
            xml.SelectSingleNode(root.ToString());
            //添加
            xml.AppendChild(root);

            for (int i = 0; i < 3; i++)
            {
                //创建根节点的子节点
                XmlElement keyNode = xml.CreateElement("Language");
                keyNode.SetAttribute("key", "test" + i);

                //创建子节点
                XmlElement chineseElement = xml.CreateElement("chinese");
                XmlElement chineseTraditionalElement = xml.CreateElement("chineseTraditional");
                XmlElement englishElement = xml.CreateElement("english");
                XmlElement japaneseElement = xml.CreateElement("japanese");
                XmlElement koreanElement = xml.CreateElement("korean");
                XmlElement vietnameseElement = xml.CreateElement("vietnamese");

                chineseElement.InnerText = "*****";
                chineseTraditionalElement.InnerText = "*****";
                englishElement.InnerText = "*****";
                japaneseElement.InnerText = "*****";
                koreanElement.InnerText = "*****";
                vietnameseElement.InnerText = "*****";

                keyNode.AppendChild(chineseElement);
                keyNode.AppendChild(chineseTraditionalElement);
                keyNode.AppendChild(englishElement);
                keyNode.AppendChild(japaneseElement);
                keyNode.AppendChild(koreanElement);
                keyNode.AppendChild(vietnameseElement);

                root.AppendChild(keyNode);
            }

            //文件保存
            xml.Save(Application.dataPath.Replace("Assets", path) + "/PackageLocalizationTable.xml");

            AssetDatabase.Refresh();
        }
    }
}