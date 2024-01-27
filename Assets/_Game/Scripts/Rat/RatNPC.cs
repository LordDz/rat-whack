using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatNPC : MonoBehaviour
    {
        [SerializeField] RatMovement ratMovement;

        public float desiredX = 0;
        public float desiredY = 0;

        public RatMovement RatMovement { get { return ratMovement; } }
    }
}