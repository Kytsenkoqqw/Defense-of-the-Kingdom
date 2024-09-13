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
    private StateManager _stateManager;

    private void Start()
    {
        _stateManager = GetComponent<StateManager>();
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(BuyGuard);
    }

    private void BuyGuard()
    {
         var newGuard = Instantiate(_guardPrefab, _spawnPoint.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(BuyGuard);
    }
}
