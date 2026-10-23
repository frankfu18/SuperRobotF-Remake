using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFullScreenAdapt : MonoBehaviour
{
    public enum EAdaptDirection
    {
        All,
        Width,
        Height,
        Auto,
    }

    public EAdaptDirection AdaptDirection = EAdaptDirection.All;

    private RectTransform rectTransform;
    private Image image;
    private RawImage rawImage;
    private Texture texture;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        rawImage = GetComponent<RawImage>();
        texture = null;
    }
    
    void Update()
    {
        if (image != null && image.mainTexture != texture)
        {
            texture = image.mainTexture;
            AdaptScreen();
        }

        if (rawImage != null && rawImage.mainTexture != texture)
        {
            texture = rawImage.mainTexture;
            AdaptScreen();
        }
    }

    private void AdaptScreen()
    {
        if (texture == null)
        {
            return;
        }

        if (AdaptDirection == EAdaptDirection.All)
        {
            rectTransform.sizeDelta = new Vector2(texture.width, texture.height);
        }
        else if (AdaptDirection == EAdaptDirection.Width)
        {
            float radio = Screen.width / (float)texture.width;

            rectTransform.sizeDelta = new Vector2(texture.width, Screen.height * radio);
        }
        else if (AdaptDirection == EAdaptDirection.Height)
        {
            float radio = Screen.height / (float)texture.height;

            rectTransform.sizeDelta = new Vector2(texture.width * radio, Screen.height);
        }
        else if (AdaptDirection == EAdaptDirection.Auto)
        {
            float widthRadio = Screen.width / (float)texture.width;
            float heightRadio = Screen.height / (float)texture.height;

            if (widthRadio >= heightRadio)
            {
                rectTransform.sizeDelta = new Vector2(Screen.width, texture.height * widthRadio);
            }
            else
            {
                rectTransform.sizeDelta = new Vector2(texture.width * heightRadio, Screen.height);
            }
        }
    }
}
