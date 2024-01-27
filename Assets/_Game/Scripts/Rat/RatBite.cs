using Assets._Game.Scripts.Player;
using Assets._Game.Scripts.Score;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatBite : MonoBehaviour
    {
        [SerializeField] AudioSource audioScratch;
        [SerializeField] GameObject bloodPrefab;

        void OnMouseEnter()
        {
            DealDamage();
            SpawnBlood();
        }

        private void DealDamage()
        {
            PlayerHealth.instance.TakeDamage();
        }

        private void SpawnBlood()
        {
            var blood = Instantiate(bloodPrefab);
            blood.transform.position = new Vector3(transform.position.x, transform.position.y, 2);
        }
    }
}