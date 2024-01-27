using Assets._Game.Scripts.Player;
using Assets._Game.Scripts.Score;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatBite : MonoBehaviour
    {
        [SerializeField] float bloodOffsetX = 3;
        [SerializeField] float bloodOffsetY = 3;
        [SerializeField] List<GameObject> bloodPrefabs;
        [SerializeField] bool canBite = false;
        [SerializeField] RatNPC npc;

        void OnMouseEnter()
        {
            if (!canBite || npc.RatPickup.isCarringItem) return;
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
            var rand = (int)UnityEngine.Random.Range(1, 3);
            int e1 = 0, e2 = 0, e3 = 0;
            switch (rand)
            {
                case 1:
                    e1 = 1;
                    e2 = 2;
                    e3 = 3;
                
                    break;
                case 2:
                    e1 = 3;
                    e2 = 2;
                    e3 = 1;

                    break;
                case 3:
                    e1 = 0;
                    e2 = 2;
                    e3 = 1;

                    break;
            }
            SpawnEffect(0, e1);
            SpawnEffect(1, e2);
            SpawnEffect(2, e3);
        }

        private void SpawnEffect(int i, int offset)
        {
            var blood = Instantiate(bloodPrefabs[i]);
            blood.transform.position = new Vector3(transform.position.x + (offset * bloodOffsetX + UnityEngine.Random.Range(0, bloodOffsetX)), transform.position.y + (UnityEngine.Random.Range(-bloodOffsetY, bloodOffsetY)), 2);
        }
    }
}