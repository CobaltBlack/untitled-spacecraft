using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Makes collider same size as sprite
// from:
// https://forum.unity.com/threads/changing-boxcollider2d-size-to-match-sprite-bounds-at-runtime.267964/
[ExecuteInEditMode]
public class ColliderFitSprite : MonoBehaviour
{
    void Awake()
    {
        runInEditMode = true;
    }

    void Update()
    {
        var _sprite = FindObjectOfType<SpriteRenderer>();
        var _collider = FindObjectOfType<BoxCollider2D>();

        _collider.offset = new Vector2(0, 0);
        _collider.size = new Vector3(_sprite.bounds.size.x / transform.lossyScale.x,
                                     _sprite.bounds.size.y / transform.lossyScale.y,
                                     _sprite.bounds.size.z / transform.lossyScale.z);
    }
}
