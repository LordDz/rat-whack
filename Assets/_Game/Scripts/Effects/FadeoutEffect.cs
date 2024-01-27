﻿using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Effects
{
    public class FadeoutEffect : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] float fadeOut = 0.1f;

        // Update is called once per frame
        void Update()
        {
            Color color = spriteRenderer.color;
            float a = color.a - (fadeOut * Time.deltaTime);
            Debug.Log("a: " + a);

            spriteRenderer.color = new Color(color.r, color.g, color.b, a);

            if (spriteRenderer.color.a <= 0)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}