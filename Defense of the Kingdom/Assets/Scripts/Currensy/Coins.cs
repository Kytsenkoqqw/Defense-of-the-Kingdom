using System;
using TMPro;
using UnityEngine;

namespace Currensy
{
    public class Coins : Currensy<int>
    {
        public override event Action<int> OnValueChangedEvent;

        public Coins()
        {
            this.value = 0;
        }

        public override void AddCurrency(int amount)
        {
            value += amount;
            OnValueChangedEvent?.Invoke(value);
        }

        public override void SpendCurrency(int amount)
        {
            if (value >= amount)
            {
                value -= amount;
                OnValueChangedEvent?.Invoke(value);
            }
            else
            {
                Debug.Log("Недостаточно  валюты");
            }
        }
    }
}