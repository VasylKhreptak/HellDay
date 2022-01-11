﻿using UnityEngine;

public class AdaptedSpriteRenderer : ColorAdapter
{
    [SerializeField] private SpriteRenderer _adaptee;

    public override Color color
    {
        get => _adaptee.color;
        set => _adaptee.color = value;
    }
}