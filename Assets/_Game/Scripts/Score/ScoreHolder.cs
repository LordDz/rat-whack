using UnityEngine;

namespace Assets._Game.Scripts.Score
{
    public class ScoreHolder : MonoBehaviour
    {
        public static ScoreHolder Instance;
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
        }

        public void AddTickled()
        {
            timesTickled += 1;
        }

        public void AddBitten()
        {
            timesBitten += 1;
        }

        public void AddFoodPickedUp()
        {
            timesPickedUp += 1;
            //Debug.Log("Pickups: " + timesPickedUp);

        }

        public void AddFoodCollected()
        {
            timesFoodBroughtToPiles += 1;
            //Debug.Log("Food: " + timesFoodBroughtToPiles);
        }
    }
}