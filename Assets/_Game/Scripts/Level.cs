using Assets._Game.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts
{
    public class Level : MonoBehaviour
    {
        public static Level instance;
        public int levelIndex = 0;
        [SerializeField] float timeDivider = 0.2f;
        [SerializeField] AudioSource levelMusic;
        [SerializeField] UIStat statLevel;
        [SerializeField] UIStat statTime;

        public float cooldown = 30f;
        public float timeWait = 30f;

        private bool levelStarted = false;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            levelMusic.Play();
            LevelStart();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit();
            }

            cooldown -= Time.deltaTime;

            int seconds = (int)cooldown;
            statTime.SetValue(seconds);

            if (levelStarted && cooldown <= 0)
            {
                LevelDone();
            }
        }

        private void LevelStart()
        {
            if (levelIndex > 0 && levelIndex < 20)
            {
                Time.timeScale = 1 + (levelIndex * timeDivider);
                levelMusic.pitch = 1 + (levelIndex * timeDivider);
            }
            Debug.Log("Level Started with timescale: " + Time.timeScale);
            Debug.Log("Music length:: " + levelMusic.time);

            levelIndex++;
            levelStarted = true;
            cooldown = timeWait;

            statLevel.SetValue(levelIndex);
        }

        private void LevelDone()
        {
            Debug.Log("Level done");
            LevelStart();
        }
    }
}