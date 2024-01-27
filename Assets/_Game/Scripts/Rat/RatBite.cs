using Assets._Game.Scripts.Score;
using UnityEngine;

namespace Assets._Game.Scripts.Rat
{
    public class RatBite : MonoBehaviour
    {
        [SerializeField] AudioSource audioScratch;


        void OnMouseEnter()
        {
            CheckScratches();
        }
       
    }
}