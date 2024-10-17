using System;
using System.Collections;
using System.Collections.Generic;
using Currensy;
using DG.Tweening;
using DoTween;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuyingTower : MonoBehaviour
{
    public static BuyingTower Instance { get; private set; }
    public Action TowerSpawn;
    [SerializeField] private Button _buyTowerButton;
    [SerializeField] private GameObject _towerPlacementPrefab; // Префаб башни с логикой размещения
    [SerializeField] private Coins _coins;
    [SerializeField] private Image _towerRedAlert;
    [SerializeField] private GameObject _buildingPanel; // Панель с кнопкой покупки

    private int _towerPrice = 10;
    private bool _yoyTime;
    public GameObject SpawnedTower { get;  set; }
    private bool _isPlacingTower;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        _buyTowerButton.onClick.AddListener(BuyTower);
    }

    public void BuyTower()
    {
        if (_yoyTime)
            return;

        if (_coins.value < _towerPrice)
        {
            _yoyTime = true;
            _towerRedAlert.DOKill();
            _towerRedAlert.DOColor(Color.red, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
                _yoyTime = false;
            });
            Debug.Log("Не хватает золота");
        }
        else
        {
            _coins.SpendCurrency(_towerPrice);
            SpawnedTower = Instantiate(_towerPlacementPrefab); // Спавним башню
            TowerSpawn?.Invoke();

        }
    }

    private void OnDisable()
    {
        _buyTowerButton.onClick.RemoveListener(BuyTower);
    }
}
