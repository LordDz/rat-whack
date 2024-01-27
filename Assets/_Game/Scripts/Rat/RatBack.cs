using Assets._Game.Scripts.Score;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatBack : MonoBehaviour
    {
        [SerializeField] AudioSource audioScratch;
        [SerializeField] AudioSource audioWhacked;
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

        void OnMouseEnter()
        {
            if (!npc.RatMovement.IsWhacked)
            {
                scratches++;
                CursorSprite.instance.StartScratching(true);
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
                    cooldownScratch = scratchWait;

                    //spriteRenderer.color = Color.blue;
                    RatScratched();
                    break;

                case 3:
                    cooldownScratch = scratchWait;

                    //spriteRenderer.color = Color.red;
                    RatScratched();
                    break;

                case 4:
                    //spriteRenderer.color = Color.white;
                    RatWhacked();
                    break;
            }
        }

        private void RatScratched()
        {
            ScoreHolder.Instance.AddScratch();
            ratSpriteChanger.StartChanging();

            movement.RatScratched();
            audioScratch.Play();
        }

        private void RatWhacked()
        {
            scratches = 0;
            cooldownScratch = 0;
            RatManager.instance.RatDespawned(npc);
            npc.RatMovement.RatWhacked();
            audioWhacked.Play();

            ScoreHolder.Instance.AddWhacked();
        }
    }
}