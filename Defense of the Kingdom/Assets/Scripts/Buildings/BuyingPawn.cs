using System;
using Currensy;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class BuyingPawn : MonoBehaviour
    {
        [SerializeField] private Button _buyPawnButton;
        [SerializeField] private GameObject _pawnPrefab;
        [SerializeField] private Transform _pawnSpawnPoint;
        [SerializeField] private int _pricePawn = 3;
        [SerializeField] private  Coins _coins;

        private void OnEnable()
        {
            _buyPawnButton.onClick.AddListener(PawnBuy);
        }

        private void PawnBuy()
        {
            if (_coins.value < 3)
            {
                Debug.Log("nema zolota");
            }
            else
            {
                _coins.SpendCurrency(_pricePawn);
                Instantiate(_pawnPrefab, _pawnSpawnPoint.position, Quaternion.identity);
            }
           
        }

        private void OnDisable()
        {
            _buyPawnButton.onClick.RemoveListener(PawnBuy);
        }
    }
}