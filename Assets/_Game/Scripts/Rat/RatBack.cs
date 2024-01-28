using Assets._Game.Scripts.Score;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatBack : MonoBehaviour
    {
        [SerializeField] AudioSource audioScratch;
        [SerializeField] AudioSource audioTickled;
        [SerializeField] RatNPC npc;
        [SerializeField] RatMovement movement;
        [SerializeField] RatSpriteChanger ratSpriteChanger;
        [SerializeField] SpriteRenderer spriteRenderer;

        private float scratchWait = 1.5f;
        private float cooldownScratch = 0;

        public int scratches = 0;

        private void ResetScratches()
        {
            scratches = 0;
            CheckScratches();
        }

        private void Update()
        {
            if (scratches > 0 && cooldownScratch > 0)
            {
                cooldownScratch -= Time.deltaTime;
                if (cooldownScratch <= 0)
                {
                    ResetScratches();
                }
            }
        }

        private void OnMouseOver()
        {
            if (!npc.RatPickup.IsCarringItem && !npc.RatMovement.IsTickled && CursorSprite.instance.IsMouseDown)
            {
                scratches++;
                CursorSprite.instance.StartScratching(true);
                CheckScratches();
            }
        }

        private void OnMouseEnter()
        {
            if (!npc.RatPickup.IsCarringItem && !npc.RatMovement.IsTickled)
            {
                CursorSprite.instance.StartScratching(true);
                scratches++;
                CheckScratches();
            }
        }


        private void CheckScratches()
        {
            switch (scratches)
            {
                case 0:
                    cooldownScratch = scratchWait;
                    //spriteRenderer.color = Color.white;
                    break;

                case 1:
                    cooldownScratch = scratchWait;

                    //spriteRenderer.color = Color.green;
                    RatScratched();
                    break;

                case 2:
                    //cooldownScratch = scratchWait;

                    //spriteRenderer.color = Color.blue;
                    RatScratched();

                    //RatScratched();
                    break;

                case 3:
                    cooldownScratch = scratchWait;

                    //spriteRenderer.color = Color.red;
                    RatTickled();
                    break;

                    //case 4:
                    //    //spriteRenderer.color = Color.white;
                    //    break;
            }
        }

        private void RatScratched()
        {
            ScoreHolder.Instance.AddScratch();
            ratSpriteChanger.StartChanging();

            movement.RatScratched();
            audioScratch.Play();
        }

        private void RatTickled()
        {
            scratches = 0;
            cooldownScratch = 0;
            RatManager.instance.RatDespawned(npc);
            npc.RatMovement.RatTickled();

            if (audioTickled != null)
            {
                audioTickled.Play();
            }

            ScoreHolder.Instance.AddTickled();
        }
    }
}