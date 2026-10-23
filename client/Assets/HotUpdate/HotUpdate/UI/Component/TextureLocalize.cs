using UnityEngine;
using UnityEngine.UI;
using editor.cfg.localize;

[RequireComponent(typeof(RectTransform))]
public class TextureLocalize : MonoBehaviour
{
    public string key;

    private Image image;
    private RawImage rawImage;
    private RectTransform rectTransform;

    private void Awake()
    {
        image = GetComponent<Image>();
        rawImage = GetComponent<RawImage>();
        rectTransform = this.transform as RectTransform;
    }

    private void Start()
    {
        SetLocalizeTexture(FrankGame.LocalizationManager.Instance.Lanuage);
    }

    public void SetLocalizeTexture(SystemLanguage _language)
    {
        if (string.IsNullOrEmpty(key))
        {
            return;
        }

        var texLanguageCfg = ConfigSystem.CurrentData.GetTextureLanguage(key);
        if (texLanguageCfg == null)
        {
            return;
        }

        TexLocalizeElement texLocalizeElement;
        if (!texLanguageCfg.localizeEleMap.TryGetValue((ELanguageType)_language, out texLocalizeElement))
        {
            texLocalizeElement = texLanguageCfg.defaultEle;
        }

        ApplyTexElement(texLocalizeElement);
    }

    private void ApplyTexElement(TexLocalizeElement _texEle)
    {
        switch (_texEle.GetTypeId())
        {
            case TexAssetElement.TYPE_ID:
                {
                    TexAssetElement ele = (TexAssetElement)_texEle;
                    if (image != null)
                    {
                        AssetHandler.Instance.SetSpriteById(ele.asset, image, ele.nativeSize);
                    }
                    if (rawImage != null)
                    {
                        AssetHandler.Instance.SetTextureById(ele.asset, rawImage, ele.nativeSize);
                    }
                }
                break;
            case TexPositionElement.TYPE_ID:
                {
                    TexPositionElement ele = (TexPositionElement)_texEle;
                    Vector3 pos = this.rectTransform.localPosition;
                    pos.x = ele.pos.x;
                    pos.y = ele.pos.y;

                    this.rectTransform.localPosition = pos;
                }
                break;
            case TexScaleElement.TYPE_ID:
                {
                    TexScaleElement ele = (TexScaleElement)_texEle;
                    Vector3 scale = this.rectTransform.localScale;
                    scale.x = ele.scale.x;
                    scale.y = ele.scale.y;

                    this.rectTransform.localScale = scale;
                }
                break;
            case TexSizeElement.TYPE_ID:
                {
                    TexSizeElement ele = (TexSizeElement)_texEle;
                    Vector2 size = new Vector2(ele.size.x, ele.size.y);

                    this.rectTransform.sizeDelta = size;
                }
                break;
            case TexLocalizeElements.TYPE_ID:
                {
                    TexLocalizeElements ele = (TexLocalizeElements)_texEle;
                    foreach (var element in ele.elements)
                    {
                        ApplyTexElement(element);
                    }
                }
                break;
            default:
                LogUtil.Error("未找到对应的图片本地化类型：" + _texEle.GetType().Name);
                break;
        }
    }
}
