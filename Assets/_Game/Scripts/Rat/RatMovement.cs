using Assets._Game.Scripts.Items;
using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatMovement : MonoBehaviour
    {
        [SerializeField] float ratSpeedX = 0.1f;
        [SerializeField] float ratSpeedY = 0.1f;

        [SerializeField] float tickledSpeedX = 1f;
        [SerializeField] float tickledSpeedY = 1f;

        [SerializeField] float timeScratches = 0.5f;

        [SerializeField] RatBack ratBack;
        [SerializeField] RatPickup pickup;

        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] bool followMouseCursor = false;

        [SerializeField] float zRotMin;
        [SerializeField] float zRotMax;
        [SerializeField] float zRotSpeed;

        private float rotZ = 0;
        private int zRotDirection = 1;

        public bool IsTickled { get; private set; }

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
            cooldownWait = timeScratches;
        }

        public void RatTickled()
        {
            IsTickled = true;
            GoToClosestFoodPile();
        }

        private void RotateZ()
        {
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
            rotZ += zRotSpeed * zRotDirection;

            if (rotZ > zRotMax)
            {
                zRotDirection = -1;
            }
            else if (rotZ < zRotMin)
            {
                zRotDirection = 1;
            }
        }


        private void Update()
        {
            if (transform.position.x == desiredX && transform.position.y == desiredY)
            {
                SetRandomXY();
            }
            else
            {
                if (cooldownWait <= 0)
                {
                    MoveTowardPos(transform.position);
                    RotateZ();
                    Debug.DrawLine(transform.position, new Vector3(desiredX, desiredY, transform.position.z), Color.red);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    cooldownWait -= Time.deltaTime;
                }
            }
        }

        private void SetRandomXY()
        {
            if (followMouseCursor)
            {
                var mousePos = Input.mousePosition;
                mousePos = new Vector3(-mousePos.x, -mousePos.y, -2);
                Debug.Log("MousePos1: " + mousePos);
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                Debug.Log("MousePos2: " + mousePos);

                SetDesiredXY(mousePos.x, mousePos.y);
                IsTickled = false;
                return;
            }
            int range = RatSpawner.instance.spawnPoints.Count - 1;
            Vector3 pos = RatSpawner.instance.spawnPoints[Random.Range(0, range)].transform.position;

            SetDesiredXY(pos.x, pos.y);
            IsTickled = false;

            if (pickup.IsCarringItem)
            {
                ItemPickupManager.instance.EggDropped(pickup.ItemPickedUp);
                pickup.ItemDrop();
            }
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
                    lowestDist = dist;
                    selectedIndex = i;
                }
            }
            Vector3 pos = RatFoodPiles.instance.foods[selectedIndex].transform.position;
            SetDesiredXY(pos.x, pos.y);
        }

        private void MoveTowardPos(Vector3 pos)
        {
            float x = IsTickled ? tickledSpeedX * Time.deltaTime : ratSpeedX * Time.deltaTime;
            float y = IsTickled ? tickledSpeedY * Time.deltaTime : ratSpeedY * Time.deltaTime;


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
