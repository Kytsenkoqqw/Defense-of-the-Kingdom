using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Currensy
{
    public class TakeCurrensy : MonoBehaviour
    {
        [SerializeField] private Coins _coins;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<DestroyCurrensy>())
            {
                _coins.AddCurrency(5);
            }
        }
    }
}