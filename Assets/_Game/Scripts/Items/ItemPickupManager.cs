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

        private float pileX = 1;
        private float pileY = 1;


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
                if (rat.RatPickup.isCarringItem || rat.RatMovement.IsWhacked) continue;

                Vector3 ratPos = rat.RatPickup.transform.position;
                for (int i = 0; i < pickupsActive.Count; i++)
                {
                    Vector3 pickupPos = pickupsActive[i].transform.position;

                    bool isInRangeX = pickupPos.x - sizeX < ratPos.x && ratPos.x < pickupPos.x + sizeX;
                    bool isInRangeY = pickupPos.y - sizeY < ratPos.y && ratPos.y < pickupPos.y + sizeY;

                    if (isInRangeX && isInRangeY)
                    {
                        //Collision!
                        ItemPickup pickup = pickupsActive[i];
                        pickup.EggPickedUp(rat.RatPickup);
                        rat.RatPickup.ItemPickup(pickup);
                        pickupsActive.Remove(pickup);
                        pickupsCarriedByRats.Add(pickup);
                        ScoreHolder.Instance.AddFoodPickedUp();

                        return;
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
                Debug.Log("foodpos: " + foodPos);
                for (int i = 0; i < pickupsCarriedByRats.Count; i++)
                {
                    Vector3 pickupPos = pickupsCarriedByRats[i].transform.position;

                    bool isInRangeX = foodPos.x - pileX < pickupPos.x && pickupPos.x < foodPos.x + pileX;
                    bool isInRangeY = foodPos.y - pileY < pickupPos.y && pickupPos.y < foodPos.y + pileY;

                    if (isInRangeX && isInRangeY)
                    {
                        ItemPickup pickup = pickupsCarriedByRats[i];
                        pickup.EggDropped();
                        pickupsCarriedByRats.Remove(pickup);
                        pickup.transform.SetParent(null);
                        pickup.EggDropped();
                        ScoreHolder.Instance.AddFoodCollected();

                        return;
                    }
                }
            }
        }
    }
}