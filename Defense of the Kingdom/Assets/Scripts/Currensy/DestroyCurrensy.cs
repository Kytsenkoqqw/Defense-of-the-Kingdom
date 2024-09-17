using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Currensy
{
    public class DestroyCurrensy : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                Destroy(gameObject);
            }
        }
    }
}