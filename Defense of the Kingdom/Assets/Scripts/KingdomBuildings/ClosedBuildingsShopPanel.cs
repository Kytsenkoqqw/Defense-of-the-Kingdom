using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DoTween
{
    public class ClosedBuildingsShopPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _buildingsPanel;
        [SerializeField] private Button _closedBuildingsButton;
        [SerializeField] private Transform _transformBuildingsPanel;
        
        private void OnEnable()
        {
            _closedBuildingsButton.onClick.AddListener(ClosedBuildingsPanel);
        }
        
        private void ClosedBuildingsPanel()
        {
            _transformBuildingsPanel.DOScale(new Vector3(0, 0, 0), 0.5f);
            _buildingsPanel.SetActive(false);
        }
    }
}