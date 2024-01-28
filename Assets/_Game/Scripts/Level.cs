using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts
{
    public class Level : MonoBehaviour
    {
        public int levelIndex = 0;
        float timeDivider = 0.1f;
        [SerializeField] AudioSource levelMusic;

        private bool levelStarted = false;

        private void Start()
        {
            LevelStart();
        }

        private void Update()
        {
            if (levelStarted && !levelMusic.isPlaying)
            {
                LevelDone();
            }
        }

        private void LevelStart()
        {
            levelMusic.Play();
            if (levelIndex > 0)
            {
                Time.timeScale = 1 + (levelIndex / timeDivider);
            }
            Debug.Log("Level Started with timescale: " + Time.timeScale);

            levelIndex++;
            levelStarted = true;
        }

        private void LevelDone()
        {
            Debug.Log("Level done");
            LevelStart();
        }
    }
}