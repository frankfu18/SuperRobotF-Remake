using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/SdkConfig", fileName = "SdkConfig")]
    public class SdkConfig : SingleScriptableObject<SdkConfig>
    {
        [Header("广告开关")]
        public bool PlayAd = false;

        [Header("商品购买开关")]
        public bool BuyMoney = false;

        [Header("登录方式(自定义字符串) [空(本地数据),google(谷歌登录)]")]
        public string LoginMode;
    }
}