using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShaderSetup : MonoBehaviour
{
    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            spriteRenderer.material.mainTexture = spriteRenderer.sprite.texture;
    }
}
