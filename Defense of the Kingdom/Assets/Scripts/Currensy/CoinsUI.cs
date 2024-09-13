using System;
using TMPro;
using UnityEngine;

namespace Currensy
{
    public class CoinsUI : MonoBehaviour
    {
        [SerializeField] private Coins _coins;
        [SerializeField] private TextMeshProUGUI _coinsText;
        

        private void OnEnable()
        {
            _coins.OnValueChangedEvent += UpdateUI;
        }

        private void OnDisable()
        {
            _coins.OnValueChangedEvent -= UpdateUI;
        }

        private void UpdateUI(int newValue)
        {
            _coinsText.text = " " + newValue;
        }
    }
}