using System;
using System.Collections;
using System.Collections.Generic;
using Currensy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class BuyingGuard : MonoBehaviour
{
    [Inject] private Transform[] _waypoints;
    [Inject] private PolygonCollider2D[] _attackAreas;
    [SerializeField] private GameObject _guardPrefab;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Coins _coins;
    [SerializeField] private int  _priceGuard = 5;


    private void OnEnable()
    {
        _buyButton.onClick.AddListener(BuyGuard);
    }

    private void BuyGuard()
    {
        if (_coins.value < 5)
        {
            Debug.Log("не хватает золота");
        }
        else
        {
            _coins.SpendCurrency(_priceGuard);
            var newGuard = Instantiate(_guardPrefab, _spawnPoint.position, Quaternion.identity);
            var stateManager = newGuard.GetComponent<StateManager>();
            stateManager.InjectDependencies(_waypoints, _attackAreas);
            // Включаем начальное состояние IdleState
            stateManager.ChangeState(new GuardIdleState(stateManager.transform, stateManager.GetAnimator(), _waypoints, stateManager, _attackAreas));
            
        }
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(BuyGuard);
    }
}
