using System;
using System.Collections;
using System.Collections.Generic;
using Currensy;
using DG.Tweening;
using DoTween;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuyingBuilding : MonoBehaviour
{
    public static BuyingBuilding Instance { get; private set; }
    public Action BuildingSpawn;
  //  [SerializeField] private Button _buyTowerButton;
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private int  _towerPrice;
    [SerializeField] private GameObject _housePrefab;
    [SerializeField] private int _housePrice;
    [SerializeField] private Coins _coins;
    [SerializeField] private Image _redAlert;

    //  [SerializeField] private int _buildingPrice;
    private bool _yoyTime;
    public GameObject SpawnedBuilding { get; set; }
    private bool _isPlacingBuildibg;
    
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void BuyingTower()
    {
        RedAlert(_towerPrice);
        
        _coins.SpendCurrency(_towerPrice);
        SpawnedBuilding = Instantiate(_towerPrefab); 
        BuildingSpawn?.Invoke();
        
    }

    public void BuyingHouse()
    {
        RedAlert(_housePrice);
        _coins.SpendCurrency(_housePrice);
        SpawnedBuilding = Instantiate(_housePrefab);
        BuildingSpawn?.Invoke();
    }

    private void RedAlert(int price)
    {
        if (_coins.value < price)
        {
            if (_yoyTime) return;

            _yoyTime = true;
            _redAlert.DOKill();
            _redAlert.DOColor(Color.red, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(() => { _yoyTime = false; });
            Debug.Log("Не хватает золота");
        } 
    }
    
}

