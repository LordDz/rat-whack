using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Game.Scripts.Menu
{
    public class SceneChanger : MonoBehaviour
    {
        public void ChangeToGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}