using System;
using UnityEngine;

namespace Currensy
{
    public class Wood : Currensy<int>
    {
        public override event Action<int> OnValueChangedEvent;

        public Wood()
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
                Debug.Log("Недостаточно валюты");
            }
        }
    }
}