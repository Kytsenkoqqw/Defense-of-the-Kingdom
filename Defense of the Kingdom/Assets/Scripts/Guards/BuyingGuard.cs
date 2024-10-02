using System;
using System.Collections;
using System.Collections.Generic;
using Currensy;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class BuyingGuard : MonoBehaviour
{
    [Inject] private Transform[] _waypoints;
    [Inject(Id = "GuardAttackAreas")]
    private PolygonCollider2D[] _attackAreas;
    [SerializeField] private GameObject _guardPrefab;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Coins _coins;
    [SerializeField] private int  _priceGuard = 5;
    [SerializeField] private Image _redAlert;
    private bool _yoyTime = false;
    
    private Transform _torchTransform;
    

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(BuyGuard);
    }

    private void BuyGuard()
    {
        if (_yoyTime)
            return;

        if (_coins.value < 5)
        {
            _yoyTime = true; // Устанавливаем флаг, что анимация началась
            _redAlert.DOKill(); // Остановка текущей анимации, если она есть
            _redAlert.DOColor(Color.red, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(() => 
            {
                _yoyTime = false; // Сбрасываем флаг по завершению анимации
            });
            Debug.Log("не хватает золота");
        }
        else
        {
            _coins.SpendCurrency(_priceGuard);
            var newGuard = Instantiate(_guardPrefab, _spawnPoint.position, Quaternion.identity);
            var stateManager = newGuard.GetComponent<StateManager>();
            stateManager.InjectDependencies(_waypoints, _attackAreas);
            // Включаем начальное состояние IdleState
            stateManager.ChangeState(new GuardIdleState(stateManager.transform, stateManager.GetAnimator(), _waypoints, stateManager, _attackAreas, _torchTransform));
            
        }
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(BuyGuard);
    }
    
    
}
