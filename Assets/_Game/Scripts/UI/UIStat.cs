using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Game.Scripts.UI
{
    public class UIStat : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textLabel;
        [SerializeField] TextMeshProUGUI textValue;
        
        public void SetValue(int val)
        {
            textValue.text = val.ToString();
        }
    }
}