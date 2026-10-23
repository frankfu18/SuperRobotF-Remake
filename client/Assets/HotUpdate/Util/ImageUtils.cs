
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ImageUtils
    {
        /// <summary>
        /// 替换图片之后重新按比例更新图片的宽高
        /// 因为不同类型的图片大小不一致，所以设置完图片后需要重新设置宽高
        /// </summary>
        /// <param name="image"></param>
        /// <param name="spriteId"></param>
        /// <param name="forceAdapt">强制适配，sprite 小于最大宽高时，强制适配成最大宽高</param>
        public static void SetImageAndUpdateSize(Image image, string spriteId, bool forceAdapt = false)
        {
            if (image == null || !image.gameObject.activeSelf)
            {
                return;
            }
            //  Image 先定义一个最大宽高
            var maxSize = image.GetComponent<RectTransform>().sizeDelta;
            //  替换图片，按照原比例放图片
            AssetHandler.Instance.SetSpriteById(spriteId, image, true);
            //  按最大宽高比例缩放图片
            var nowSize = image.GetComponent<RectTransform>().sizeDelta;
            //  原图太小了，不缩放了
            if (nowSize.x < maxSize.x && nowSize.y < maxSize.y && !forceAdapt)
            {
                return;
            }
            
            if (nowSize.x >= nowSize.y)
            {
                var r = maxSize.x / nowSize.x;
                var newY = nowSize.y * r;
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(maxSize.x, newY);
            }
            else
            {
                var r = maxSize.y / nowSize.y;
                var newX = nowSize.x * r;
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(newX, maxSize.y);
            }
        }
        
        /// <summary>
        /// 替换图片之后重新按比例更新图片的宽高
        /// 因为不同类型的图片大小不一致，所以设置完图片后需要重新设置宽高
        /// </summary>
        /// <param name="image"></param>
        /// <param name="spriteId"></param>
        public static void SetImageAndUpdateSize(Image image, Action setImgAction)
        {
            //  Image 先定义一个最大宽高
            var maxSize = image.GetComponent<RectTransform>().sizeDelta;
            //  替换图片，按照原比例放图片
            setImgAction.Invoke();
            image.SetNativeSize();
            //  按最大宽高比例缩放图片
            var nowSize = image.GetComponent<RectTransform>().sizeDelta;
            if (nowSize.x >= nowSize.y)
            {
                var r = maxSize.x / nowSize.x;
                var newY = nowSize.y * r;
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(maxSize.x, newY);
            }
            else
            {
                var r = maxSize.y / nowSize.y;
                var newX = nowSize.x * r;
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(newX, maxSize.y);
            }
        }
        
    }
