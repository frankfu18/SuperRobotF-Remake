using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class UIRawImageSwitch : MonoBehaviour
{
    public List<Texture> textures;

    public RawImage rawImage;

    private void Awake()
    {
        rawImage = this.GetComponent<RawImage>();
    }

    public void Switch(int _idx, bool _nativeSize = false)
    {
        if (_idx < 0 || _idx >= textures.Count)
        {
            return;
        }

        if (rawImage == null)
        {
            rawImage = this.GetComponent<RawImage>();
        }

        rawImage.texture = textures[_idx];
        if (_nativeSize)
        {
            rawImage.SetNativeSize();
        }
    }
}
