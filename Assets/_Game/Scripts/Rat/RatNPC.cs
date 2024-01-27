using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatNPC : MonoBehaviour
    {
        [SerializeField] RatMovement ratMovement;
        [SerializeField] RatPickup ratPickup;

        public RatMovement RatMovement { get { return ratMovement; } }
        public RatPickup RatPickup { get { return ratPickup; } }
    }
}