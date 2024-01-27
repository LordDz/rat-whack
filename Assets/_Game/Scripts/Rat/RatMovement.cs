using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatMovement : MonoBehaviour
    {
        [SerializeField] float ratSpeedX = 0.1f;
        [SerializeField] float ratSpeedY = 0.1f;
        [SerializeField] float timeMoving = 1f;
        [SerializeField] float timeBetweenWait = 1;
        [SerializeField] float timeScratches = 0.5f;

        [SerializeField] RatBack ratBack;

        [SerializeField] SpriteRenderer spriteRenderer;

        private float cooldownMoving = 0;
        private float cooldownWait = 0;
        public float desiredX = 0;
        public float desiredY = 0;

        public void RatSpawned()
        {
            SetRandomXY();
        }

        public void RatScratched()
        {
            //TODO: Do something cool here
            cooldownMoving = 0;
            cooldownWait = timeScratches;

        }

        private void Update()
        {
            if (transform.position.x != desiredX && transform.position.y != desiredY)
            {
                if (cooldownMoving > 0)
                {
                    cooldownMoving -= Time.deltaTime;
                    
                    MoveTowardPos(transform.position);

                    if (cooldownMoving <= 0)
                    {
                        cooldownWait = timeBetweenWait;
                    }
                }
                else
                {
                    cooldownWait -= Time.deltaTime;

                    if (cooldownWait <= 0)
                    {
                        cooldownMoving = timeMoving;
                        ratBack.ResetScratches();
                    }
                }
            }
            else
            {
                SetRandomXY();
            }
        }

        private void SetRandomXY()
        {
            int range = RatSpawner.instance.spawnPoints.Count - 1;
            Vector3 pos = RatSpawner.instance.spawnPoints[Random.Range(0, range)].transform.position;

            SetDesiredXY(pos.x, pos.y);
            //Debug.Log("Setting random: " + pos);
        }

        public void SetDesiredXY(float x, float y)
        {
            desiredX = x;
            desiredY = y;
        }

        public void GoToClosestFoodPile()
        {
            float lowestDist = 9000;
            int selectedIndex = 0;

            for (int i = 0; i < RatFoodPiles.instance.foods.Count; i++)
            {
                float dist = Vector3.Distance(transform.position, RatFoodPiles.instance.foods[i].transform.position);
                
                if (dist < lowestDist)
                {
                    selectedIndex = i;
                }
            }
            Vector3 pos = RatFoodPiles.instance.foods[selectedIndex].transform.position;
            SetDesiredXY(pos.x, pos.y);
        }

        private void MoveTowardPos(Vector3 pos)
        {
            float x = ratSpeedX * Time.deltaTime;
            float y = ratSpeedY * Time.deltaTime;

            //X
            if (pos.x < desiredX)
            {
                //spriteRenderer.flipX = true;
                transform.localScale = new Vector3(-1, 1, 1);

                pos.x += x;

                if (pos.x > desiredX)
                {
                    pos.x = desiredX;
                }
            }
            else if (pos.x > desiredX)
            {
                //spriteRenderer.flipX = false;
                transform.localScale = new Vector3(1, 1, 1);

                pos.x -= x;

                if (pos.x < desiredX)
                {
                    pos.x = desiredX;
                }
            }

            //Y
            if (pos.y < desiredY)
            {
                pos.y += y;

                if (pos.y > desiredY)
                {
                    pos.y = desiredY;
                }
            }
            else if (pos.y > desiredY)
            {
                pos.y -= y;

                if (pos.y < desiredY)
                {
                    pos.y = desiredY;
                }
            }

            transform.position = pos;
        }
    }
}
