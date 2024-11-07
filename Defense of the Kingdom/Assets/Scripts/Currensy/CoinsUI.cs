using System;
using System.Net.Mime;
using TMPro;
using UnityEngine;

namespace Currensy
{
    public class CoinsUI : MonoBehaviour
    {
        [SerializeField] private Coins _coins;
        [SerializeField] private TextMeshProUGUI _coinsText;

        [SerializeField] private Wood _wood;
        [SerializeField] private TextMeshProUGUI _woodText;
        
    
        private void OnEnable()
        {
            _coins.OnValueChangedEvent += UpdateCoinsUI;
            _wood.OnValueChangedEvent += UpdateGemsUI;

            // Инициализируем UI текущими значениями при включении
            UpdateCoinsUI(_coins.value);
            UpdateGemsUI(_wood.value);
        }

        private void OnDisable()
        {
            _coins.OnValueChangedEvent -= UpdateCoinsUI;
            _wood.OnValueChangedEvent -= UpdateGemsUI;
        }

        private void UpdateCoinsUI(int newValue)
        {
            _coinsText.text = newValue.ToString();
        }

        private void UpdateGemsUI(int newValue)
        {
            _woodText.text = newValue.ToString();
        }
    }
}