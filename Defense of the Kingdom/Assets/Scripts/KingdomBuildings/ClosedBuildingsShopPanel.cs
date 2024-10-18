using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DoTween
{
    public class ClosedBuildingsShopPanel : MonoBehaviour
    {
        private BuyingBuilding _buyingBuilding;
        [SerializeField] private GameObject _buildingsPanel;
        [SerializeField] private Button _closedBuildingsButton;
        [SerializeField] private Transform _transformBuildingsPanel;

        private void Awake()
        {
            _buyingBuilding = FindObjectOfType<BuyingBuilding>();
        }
        
        private void OnEnable()
        {
            _closedBuildingsButton.onClick.AddListener(ClosedBuildingsPanel);
            _buyingBuilding.BuildingSpawn += ClosedBuildingsPanel;
        }
        
        private void ClosedBuildingsPanel()
        {
            _transformBuildingsPanel.DOScale(new Vector3(0, 0, 0), 0.5f);
            _buildingsPanel.SetActive(false);
        }

        private void OnDisable()
        {
            _buyingBuilding.BuildingSpawn -= ClosedBuildingsPanel;
        }
    }
}