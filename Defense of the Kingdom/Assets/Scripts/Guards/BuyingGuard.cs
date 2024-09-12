using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingGuard : MonoBehaviour
{
    [SerializeField] private GameObject _guardPrefab;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Transform _spawnPoint;
    

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(BuyGuard);
    }

    private void BuyGuard()
    {
         var newGuard = Instantiate(_guardPrefab, _spawnPoint.position, Quaternion.identity);
    }
}
