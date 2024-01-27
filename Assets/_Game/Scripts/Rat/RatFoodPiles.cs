using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatFoodPiles : MonoBehaviour
    {
        public static RatFoodPiles instance;
        public List<GameObject> foods;

        private void Awake()
        {
            instance = this;
        }
    }
}