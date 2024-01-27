using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public static PlayerHealth instance;

        private void Awake()
        {
            instance = this;
        }
    }
}