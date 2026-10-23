using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIImageSwitch : MonoBehaviour
{
    public List<Sprite> sprites;

    private Image image;

    private void Awake()
    {
        image = this.GetComponent<Image>();
    }

    public void Switch(int _idx, bool _nativeSize = false)
    {
        if (_idx < 0 || _idx >= sprites.Count)
        {
            return;
        }

        if (image == null)
        {
            image = this.GetComponent<Image>();
        }

        image.sprite = sprites[_idx];
        if (_nativeSize)
        {
            image.SetNativeSize();
        }
    }
}
