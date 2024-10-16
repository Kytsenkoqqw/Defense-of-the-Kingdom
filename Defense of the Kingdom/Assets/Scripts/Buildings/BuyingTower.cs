using System;
using System.Collections;
using System.Collections.Generic;
using Currensy;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BuyingTower : MonoBehaviour
{
    [SerializeField] private Button _buyTowerButton;
    [SerializeField] private GameObject _towerPlacementPrefab; // замените на префаб TowerPlacement
    [SerializeField] private Coins _coins;
    [SerializeField] private Image _towerRedAlert;
    private int _towerPrice = 10;

    private bool _yoyTime;

    private void OnEnable()
    {
        _buyTowerButton.onClick.AddListener(BuyTower);
    }

    private void BuyTower()
    {
        if (_yoyTime)
            return;

        if (_coins.value < _towerPrice)
        {
            _yoyTime = true; // устанавливаем флаг, что анимация началась
            _towerRedAlert.DOKill(); // остановка текущей анимации, если она есть
            _towerRedAlert.DOColor(Color.red, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
                _yoyTime = false; // сбрасываем флаг по завершению анимации
            });
            Debug.Log("не хватает золота");
        }
        else
        {
            _coins.SpendCurrency(_towerPrice);
            Instantiate(_towerPlacementPrefab, Vector2.zero, Quaternion.identity); // создаем объект для размещения башни
        }
    }

    private void OnDisable()
    {
        _buyTowerButton.onClick.RemoveListener(BuyTower);
    }
}
