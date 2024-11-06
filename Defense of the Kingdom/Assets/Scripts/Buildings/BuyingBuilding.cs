using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using Currensy;
using DG.Tweening;
using DoTween;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuyingBuilding : MonoBehaviour
{
    public int _buildingPrice;
    [SerializeField] private Transform _transformBuyingPanel;
    [SerializeField] private GameObject _buyingPanel;
    [SerializeField] private GameObject _housePrefab;
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private Coins _coins;
    [SerializeField] public int _towerPrice;
    [SerializeField] private int _housePrice;
    [SerializeField] private Image _redAlert;
    private PlacementBuilding _placementManager; 
    private bool _yoyTime;

    private void Awake()
    {
        _placementManager = FindObjectOfType<PlacementBuilding>();
    }

    public void BuyHouse()
    {
        if (_coins.value < _housePrice)
        {
            RedAlert();
            Debug.Log("Nema Zolota");
        }
        else
        {
            OffBuyingPanel();
            GameObject house = Instantiate(_housePrefab);
            _buildingPrice = _housePrice;
            _placementManager.StartPlacingBuilding(house, _housePrice);
        }
        
    }

    public void BuyTower()
    {
        if (_coins.value < _towerPrice)
        {
            RedAlert();
            Debug.Log("Nema Zolota");
        }
        else
        {
            OffBuyingPanel();
            GameObject tower = Instantiate(_towerPrefab);
            _buildingPrice = _towerPrice;
            _placementManager.StartPlacingBuilding(tower, _towerPrice);
        }
    }

    private void RedAlert()
    {
        if (_yoyTime)
            return;

        if (_coins.value < _towerPrice && _coins.value < _housePrice)
        {
            _yoyTime = true;
            _redAlert.DOKill();
            _redAlert.DOColor(Color.red, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(() => { _yoyTime = false; });
            Debug.Log("Не хватает золота");
        }
    }

    private void OffBuyingPanel()
    {
        _transformBuyingPanel.DOScale(new Vector3(0,0,0), 0.5f);
        _buyingPanel.SetActive(false);
    }

}

