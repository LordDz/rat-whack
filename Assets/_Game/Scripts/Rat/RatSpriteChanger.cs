using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatSpriteChanger : MonoBehaviour
    {
        private bool isChanging = false;

        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] List<Sprite> sprites;

        private float cooldown = 0;
        private float cooldownVísible = 0;
        [SerializeField] float waitSpriteChange = 0.0125f;
        [SerializeField] float waitVisible = 0.4f;

        private int index = 0;

        private void Update()
        {
            if (isChanging)
            {
                cooldown -= Time.deltaTime;

                if (cooldown <= 0)
                {
                    cooldown = waitSpriteChange;
                    ChangeSprite();
                }

                cooldownVísible -= Time.deltaTime;
                if (cooldownVísible <= 0)
                {
                    StopChange();
                }
            }
        }

        public void StartChanging()
        {
            cooldown = waitSpriteChange;
            cooldownVísible = waitVisible;
            isChanging = true;
        }

        private void ChangeSprite()
        {
            spriteRenderer.sprite = sprites[index];
            index++;
            
            if (index >= sprites.Count)
            {
                index = 0;
            }
        }

        private void StopChange()
        {
            spriteRenderer.sprite = sprites[0];
            isChanging = false;
        }
    }
}