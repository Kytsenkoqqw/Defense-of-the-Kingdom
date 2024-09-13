using System;
using UnityEngine;

namespace Currensy
{
    public class DestroyCurrensy : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"));
            {
                Destroy(gameObject);
            }
        }
    }
}