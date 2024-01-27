using Assets._Game.Scripts.Score;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatBack : MonoBehaviour
    {
        [SerializeField] AudioSource audioScratch;
        [SerializeField] RatNPC npc;
        [SerializeField] RatMovement movement;
        [SerializeField] SpriteRenderer spriteRenderer;

        public int scratches = 0;

        public void ResetScratches()
        {
            scratches = 0;

            CheckScratches();
        }

        void OnMouseEnter()
        {
            scratches++;
            
            CheckScratches();
        }

        private void CheckScratches()
        {
            switch (scratches)
            {
                case 0:
                    spriteRenderer.color = Color.white;
                    break;

                case 1:
                    spriteRenderer.color = Color.green;
                    RatScratched();
                    break;

                case 2:
                    spriteRenderer.color = Color.blue;

                    RatScratched();
                    break;

                case 3:
                    spriteRenderer.color = Color.red;

                    RatScratched();
                    break;

                case 4:
                    spriteRenderer.color = Color.white;
                    RatWhacked();
                    break;
            }
        }

        private void RatScratched()
        {
            ScoreHolder.Instance.AddScratch();

            movement.RatScratched();
            audioScratch.Play();
        }

        private void RatWhacked()
        {
            scratches = 0;
            RatManager.instance.RatDespawned(npc);
            ScoreHolder.Instance.AddWhacked();
        }
    }
}