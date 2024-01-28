using Assets._Game.Scripts.Items;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatPickup : MonoBehaviour
    {
        [SerializeField] Transform headParent;
        [SerializeField] RatMovement ratMovement;

        public ItemPickup ItemPickedUp { get; private set; }

        public bool IsCarringItem { get; private set; }

        public void ItemPickup(ItemPickup item)
        {
            item.transform.position = headParent.position;
            item.transform.SetParent(headParent);
            ratMovement.GoToClosestFoodPile();
            ItemPickedUp = item;
            IsCarringItem = true;
        }

        public void ItemDrop()
        {
            IsCarringItem = false;
            if (ItemPickedUp != null)
            {
                ItemPickedUp = null;
            }
        }
    }
}