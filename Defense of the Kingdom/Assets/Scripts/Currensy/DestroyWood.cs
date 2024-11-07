using System;
using UnityEngine;

namespace Currensy
{
    public class DestroyWood : MonoBehaviour
    {
        [SerializeField] private Wood _wood;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                Destroy(gameObject); 
            }
        }
    }
}