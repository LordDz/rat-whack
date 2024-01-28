using Assets._Game.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts
{
    public class Level : MonoBehaviour
    {
        public int levelIndex = 0;
        [SerializeField] float timeDivider = 0.1f;
        [SerializeField] AudioSource levelMusic;
        [SerializeField] UIStat statLevel;

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
            levelMusic.Stop();
            if (levelIndex > 0 && levelIndex < 10)
            {
                Time.timeScale = 1 + (levelIndex * timeDivider);
                levelMusic.pitch = 1 + (levelIndex * timeDivider);
            }
            Debug.Log("Level Started with timescale: " + Time.timeScale);
            Debug.Log("Music length:: " + levelMusic.time);

            levelIndex++;
            levelStarted = true;
            levelMusic.Play();
            statLevel.SetValue(levelIndex);
        }

        private void LevelDone()
        {
            Debug.Log("Level done");
            LevelStart();
        }
    }
}