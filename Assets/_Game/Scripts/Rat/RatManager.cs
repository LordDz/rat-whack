using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatManager : MonoBehaviour
    {
        public Transform ratContainer;
        public static RatManager instance;

        public List<RatNPC> ratPrefabs = new List<RatNPC>();
        private List<RatNPC> rats = new List<RatNPC>();
        public List<RatNPC> listActiveRats = new List<RatNPC>();

        private void Awake()
        {
            instance = this;

            foreach (var rat in ratPrefabs) {
                
                var newRat = Instantiate(rat);
                rats.Add(newRat);
                newRat.transform.parent = ratContainer;
                newRat.gameObject.SetActive(false);
            }
        }

        public bool AreAllRatsSpawned { get { return listActiveRats.Count > 3;} }

        public void RatSpawned(RatNPC npc)
        {
            listActiveRats.Add(npc);
        }

        public void RatDespawned(RatNPC npc)
        {
            npc.gameObject.SetActive(false);
            listActiveRats.Remove(npc);
        }

        public RatNPC GetNotActiveRat()
        {
            foreach (var npc in rats) {
                if (!listActiveRats.Contains(npc)) return npc;
            }
            return null;
        }
    }
}