using Assets._Game.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts
{
    public class Level : MonoBehaviour
    {
        public static Level instance;
        public int levelIndex = 0;
        [SerializeField] float timeDivider = 0.1f;
        [SerializeField] AudioSource levelMusic;
        [SerializeField] UIStat statLevel;
        [SerializeField] UIStat statTime;

        private float cooldown = 90f;
        [SerializeField] float timePerLevel = 90f;

        private bool levelStarted = false;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            cooldown = timePerLevel;
            levelMusic.loop = true;
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
            Debug.Log("Music pitch:: " + levelMusic.pitch);

            levelIndex++;
            levelStarted = true;
            cooldown = timePerLevel;

            statLevel.SetValue(levelIndex);
        }

        private void LevelDone()
        {
            Debug.Log("Level done");
            LevelStart();
        }
    }
}