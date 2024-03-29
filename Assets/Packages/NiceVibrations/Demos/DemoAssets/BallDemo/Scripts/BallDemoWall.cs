﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.NiceVibrations
{
    public class BallDemoWall : MonoBehaviour
    {
        protected RectTransform _rectTransform;
        protected BoxCollider2D _boxCollider2D;

        protected virtual void OnEnable()
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();
            _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();

            _boxCollider2D.size = new Vector2(_rectTransform.rect.size.x, _rectTransform.rect.size.y);
        }
    }
}