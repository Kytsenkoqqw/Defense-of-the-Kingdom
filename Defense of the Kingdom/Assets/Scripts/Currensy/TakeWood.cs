using System;
using UnityEngine;

namespace Currensy
{
    public class TakeWood : MonoBehaviour
    {
        [SerializeField] private Wood _wood;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<DestroyWood>())
            {
                _wood.AddCurrency(3);
            }
        }
    }
}