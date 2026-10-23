
using System;
using System.IO;
using UnityEngine;

namespace editor.cfg
{

    public static class CfgMgrWrapper
    {
        static string CSV_PATH = "./csv";
        private static bool loadException = false;

        public static CfgMgr CfgMgr
        {
            get
            {
                CfgMgr.XmlDir = CSV_PATH;
                if (CfgMgr.Ins == null &&!loadException)
                {                   
                    UnityEngine.Debug.Log("Reloading CfgMgr inst ...");
                    try
                    {
                        CfgMgr.Load();
                        loadException = false;
                    }
                    catch (System.Exception ex)
                    {
                        loadException = true;
                        Debug.LogException(ex);
                    }
                }
                   
                return CfgMgr.Ins;
            }
        }

        public static void Reload()
        {
            UnityEngine.Debug.Log("Reloading CfgMgr ...");
            CfgMgr.XmlDir = CSV_PATH;
            CfgMgr.Load();
        }

#if UNITY_EDITOR
        //[RuntimeInitializeOnLoadMethod]
        //public static void Reload()
        //{
        //    UnityEngine.Debug.Log("Reloading CfgMgr");           
        //    CfgMgr.Load();
        //}
#endif

        public static void SaveFile(EditorWriter cfg)
        {
            cfg.Save(Path.Combine(CfgMgr.XmlDir, cfg.FileName));
        }

        public static void DeleteFile(EditorWriter cfg)
        {
            File.Delete(Path.Combine(CfgMgr.XmlDir, cfg.FileName));
        }
       
        public static string BulletEntityFileName(string name)
        {
            return $"object/bullet/{name}.xml";
        }

        public static string AssetCurveFileName(string name)
        {
            return $"asset/animcurves/{name}.xml";
        }

        public static string AssetBezierCurveFileName(string name)
        {
            return $"asset/beziercurves/{name}.xml";
        }

        public static string SkillCutFileName(string skillCutName)
        {
            return $"skill/skillcuts/{skillCutName}.xml";
        }

        public static string SKillGroupFileName(string groupName)
        {
            return $"skill/skillgroups/{groupName}.xml";
        }

        public static string CameraLimitFileName(string limitName)
        {
            return $"inputrule/cameralimit/{limitName}.xml";
        }

        public static string SceneArtComponentFileName(string sceneArtComponent)
        {
            return $"asset/sceneartcomponents/{sceneArtComponent}.xml";
        }

        public static string BattleCameraLimitFileName(string fileName)
        {
            return $"fight/battlecamera/{fileName}.xml";
        }
    }
}
