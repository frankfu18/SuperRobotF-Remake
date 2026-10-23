using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSwitch : MonoBehaviour
{
    public List<Sprite> sprites;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void Switch(int _idx)
    {
        if (_idx < 0 || _idx >= sprites.Count)
        {
            return;
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        spriteRenderer.sprite = sprites[_idx];
    }
}
