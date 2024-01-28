using Assets._Game.Scripts.Rat;
using Assets._Game.Scripts.Score;
using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Items
{
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;

        private RatPickup ratPickup;
        //private void OnTriggerEnter(Collider other)
        //{
        //    Debug.Log("col: " + other.gameObject.name);

        //    if (other.gameObject.TryGetComponent<RatNPC>(out var rat))
        //    {
        //        //ratMovement
        //        Debug.Log("Rat collided with: " + name);
        //        collision.enabled = false;
        //        rat.RatPickup.ItemPickup(this);
        //    }
        //}

        public void EggPickedUp(RatPickup pickup)
        {
            spriteRenderer.color = Color.red;
            ratPickup = pickup;
        }

        public void EggDropped()
        {
            spriteRenderer.color = Color.blue;
            transform.SetParent(null);
            //ItemPickupManager.instance.EggDropped(this);
        }
    }
}