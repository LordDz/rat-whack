using Assets._Game.Scripts.Player;
using Assets._Game.Scripts.Score;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatBite : MonoBehaviour
    {
        [SerializeField] GameObject bloodPrefab;
        [SerializeField] bool canBite = false;

        void OnMouseEnter()
        {
            if (!canBite) return;
            DealDamage();
            SpawnBlood();
        }

        private void DealDamage()
        {
            PlayerHealth.instance.TakeDamage();
            Debug.Log("transform.parent.localScale.x: " + transform.parent.localScale.x);
            CursorSprite.instance.MoveCursor(transform.position, (int)transform.parent.localScale.x);

            ScoreHolder.Instance.AddBitten();
        }

        private void SpawnBlood()
        {
            var blood = Instantiate(bloodPrefab);
            blood.transform.position = new Vector3(transform.position.x, transform.position.y, 2);
        }
    }
}