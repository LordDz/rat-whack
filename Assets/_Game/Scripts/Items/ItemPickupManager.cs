using Assets._Game.Scripts.Rat;
using Assets._Game.Scripts.Score;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Items
{
    public class ItemPickupManager : MonoBehaviour
    {
        public List<ItemPickup> items;
        public List<ItemPickup> pickupsActive;
        public List<ItemPickup> pickupsCarriedByRats;

        private float sizeX = 2;
        private float sizeY = 4;

        private float pileX = 2;
        private float pileY = 3;


        // Update is called once per frame
        void Update()
        {
            CheckCollisions();
            CheckCarriedByRats();
        }

        private void CheckCollisions()
        {
            if (pickupsActive.Count == 0) return;

            foreach (var rat in RatManager.instance.listActiveRats)
            {
                Vector3 ratPos = rat.RatPickup.transform.position;
                for (int i = 0; i < pickupsActive.Count; i++)
                {
                    Vector3 pickupPos = pickupsActive[i].transform.position;

                    if (pickupPos.x - sizeX > ratPos.x && ratPos.x < pickupPos.x + sizeX)
                    {
                        if (pickupPos.y - sizeY > ratPos.y && ratPos.y < pickupPos.y + sizeY)
                        {
                            //Collision!
                            ItemPickup pickup = pickupsActive[i];
                            rat.RatPickup.ItemPickup(pickup);
                            pickupsActive.Remove(pickup);
                            pickupsCarriedByRats.Add(pickup);
                            ScoreHolder.Instance.AddFoodPickedUp();

                            return;
                        }
                    }
                }
            }
        }

        private void CheckCarriedByRats()
        {
            if (pickupsCarriedByRats.Count == 0) return;

            foreach (var food in RatFoodPiles.instance.foods)
            {
                Vector3 foodPos = food.transform.position;
                for (int i = 0; i < pickupsCarriedByRats.Count; i++)
                {
                    Vector3 pickupPos = pickupsCarriedByRats[i].transform.position;

                    if (pickupPos.x - pileX > foodPos.x && foodPos.x < pickupPos.x + pileX)
                    {
                        if (pickupPos.y - pileY > foodPos.y && foodPos.y < pickupPos.y + pileY)
                        {
                            ItemPickup pickup = pickupsCarriedByRats[i];
                            pickupsCarriedByRats.Remove(pickup);
                            pickup.transform.SetParent(null);
                            ScoreHolder.Instance.AddFoodCollected();

                            return;
                        }
                    }
                }
            }
        }
    }
}