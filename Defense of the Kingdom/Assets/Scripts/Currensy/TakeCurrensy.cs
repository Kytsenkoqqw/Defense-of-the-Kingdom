using System;
using UnityEngine;

namespace Currensy
{
    public class TakeCurrensy : MonoBehaviour
    {
        [SerializeField] private Coins _coins;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                _coins.AddCurrency(5);
            }
        }
    }
}