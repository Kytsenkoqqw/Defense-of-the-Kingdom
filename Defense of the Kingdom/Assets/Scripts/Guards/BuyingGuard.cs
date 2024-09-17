using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class BuyingGuard : MonoBehaviour
{
    [SerializeField] private GameObject _guardPrefab;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Transform _spawnPoint;
    [Inject] private Transform[] _waypoints;
    [Inject] private PolygonCollider2D[] _attackAreas;
    

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(BuyGuard);
    }

    private void BuyGuard()
    {
        var newGuard = Instantiate(_guardPrefab, _spawnPoint.position, Quaternion.identity);
        var stateManager = newGuard.GetComponent<StateManager>();
        if (stateManager != null)
        {
            // Инжектим необходимые данные (путевые точки и зоны атак)
            stateManager.InjectDependencies(_waypoints, _attackAreas);

            // Включаем начальное состояние IdleState
            stateManager.ChangeState(new GuardIdleState(stateManager.transform, stateManager.GetAnimator(), _waypoints, stateManager, _attackAreas));
        }
        else
        {
            Debug.LogError("StateManager not found on guard prefab!");
        }
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(BuyGuard);
    }
}
