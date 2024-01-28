using Assets._Game.Scripts.UI;
using UnityEngine;

namespace Assets._Game.Scripts.Score
{
    public class ScoreHolder : MonoBehaviour
    {
        public static ScoreHolder Instance;
        [SerializeField] UIStat statScratches;
        [SerializeField] UIStat statTickled;
        [SerializeField] UIStat statBitten;
        [SerializeField] UIStat statPickedUp;
        [SerializeField] UIStat statFoodBroughtToPiles;
        [SerializeField] AudioSource audioTickleWin;

        public int timesScratched = 0;
        public int timesTickled = 0;
        public int timesBitten = 0;
        public int timesPickedUp = 0;
        public int timesFoodBroughtToPiles = 0;

        private void Awake()
        {
            Instance = this;
        }

        public void AddScratch()
        {
            timesScratched += 1;
            statScratches.SetValue(timesScratched);
        }

        public void AddTickled()
        {
            timesTickled += 1;
            statTickled.SetValue(timesTickled);
            audioTickleWin.Play();
        }

        public void AddBitten()
        {
            timesBitten += 1;
            statBitten.SetValue(timesBitten);
        }

        public void AddFoodPickedUp()
        {
            timesPickedUp += 1;
            statPickedUp.SetValue(timesPickedUp);
        }

        public void AddFoodCollected()
        {
            timesFoodBroughtToPiles += 1;
            statFoodBroughtToPiles.SetValue(timesFoodBroughtToPiles);

            //Debug.Log("Food: " + timesFoodBroughtToPiles);
        }
    }
}