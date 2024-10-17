using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DoTween
{
    public class ClosedBuildingsShopPanel : MonoBehaviour
    {
        private BuyingTower _buyingTower;
        [SerializeField] private GameObject _buildingsPanel;
        [SerializeField] private Button _closedBuildingsButton;
        [SerializeField] private Transform _transformBuildingsPanel;

        private void Awake()
        {
            _buyingTower = FindObjectOfType<BuyingTower>();
        }
        
        private void OnEnable()
        {
            _closedBuildingsButton.onClick.AddListener(ClosedBuildingsPanel);
            _buyingTower.TowerSpawn += ClosedBuildingsPanel;
        }
        
        private void ClosedBuildingsPanel()
        {
            _transformBuildingsPanel.DOScale(new Vector3(0, 0, 0), 0.5f);
            _buildingsPanel.SetActive(false);
        }

        private void OnDisable()
        {
            _buyingTower.TowerSpawn -= ClosedBuildingsPanel;
        }
    }
}