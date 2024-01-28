using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Effects
{
    public class FadeoutEffect : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] float fadeWait = 5f;
        [SerializeField] float fadeOut = 0.1f;

        // Update is called once per frame
        void Update()
        {
            fadeWait -= Time.deltaTime;

            if (fadeWait < 0 )
            {
                Color color = spriteRenderer.color;
                float a = color.a - (fadeOut * Time.deltaTime);

                spriteRenderer.color = new Color(color.r, color.g, color.b, a);

                if (spriteRenderer.color.a <= 0)
                {
                    gameObject.SetActive(false);
                    Destroy(gameObject);
                }
            }
        }
    }
}