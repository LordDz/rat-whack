using Assets._Game.Scripts.Items;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatPickup : MonoBehaviour
    {
        [SerializeField] Transform headParent;
        [SerializeField] RatMovement ratMovement;

        public void ItemPickup(ItemPickup item)
        {
            item.transform.position = headParent.position;
            item.transform.SetParent(headParent);
            ratMovement.GoToClosestFoodPile();
        }
    }
}