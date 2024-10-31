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
    [SerializeField] private Transform _transformBuyingPanel;
    [SerializeField] private GameObject _buyingPanel;
    [SerializeField] private GameObject _buildingPrefab;
    [SerializeField] private Coins _coins;
    [SerializeField] public int _buildingPrice;
    [SerializeField] private Image _redAlert;
    private PlacementBuilding _placementManager; // Скрипт для управления перемещением зданий
    private bool _yoyTime;

    private void Awake()
    {
        _placementManager = FindObjectOfType<PlacementBuilding>();
    }

    public void BuyBuildings()
    {
        if (_coins.value < _buildingPrice)
        {
            RedAlert();
            Debug.Log("nema zolota");
        }
        else
        {
            OffBuyingPanel();
            GameObject building = Instantiate(_buildingPrefab);    
            //_coins.SpendCurrency(_buildingPrice);
            
            // Передаём объект в систему перемещения
            _placementManager.StartPlacingBuilding(building);
        }
    }

    private void RedAlert()
    {
        if (_yoyTime)
            return;

        if (_coins.value < _buildingPrice)
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

