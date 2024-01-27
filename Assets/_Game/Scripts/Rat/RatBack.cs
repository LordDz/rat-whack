using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

namespace Assets._Game.Scripts.Rat
{
    public class RatBack : MonoBehaviour
    {
        [SerializeField] RatNPC npc;

        void OnMouseOver()
        {
            RatWhacked();
        }

        private void RatWhacked()
        {
            Debug.Log(this.gameObject.name + " was whacked.");

            RatManager.instance.RatDespawned(npc);
        }
    }
}