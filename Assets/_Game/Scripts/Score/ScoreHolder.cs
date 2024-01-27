using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Score
{
    public class ScoreHolder : MonoBehaviour
    {
        public static ScoreHolder Instance;
        public int scoreCurrent = 0;

        private void Awake()
        {
            Instance = this;
        }

        public void AddScore()
        {
            scoreCurrent += 1;
        }
    }
}