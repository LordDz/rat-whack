using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] int healthDefault = 10;
        [SerializeField] float shakeDuration = 0.25f;
        [SerializeField] float shakeAmount = 4f;
        public static PlayerHealth instance;

        [SerializeField] AudioSource audioSource;

        private int health = 0;


        private void Awake()
        {
            instance = this;
            health = healthDefault;
        }

        public void TakeDamage()
        {
            audioSource.Play();
            health--;
            Debug.Log("OUCH: " + health);

            CameraShake.Shake(shakeDuration, shakeAmount);
        }
    }
}