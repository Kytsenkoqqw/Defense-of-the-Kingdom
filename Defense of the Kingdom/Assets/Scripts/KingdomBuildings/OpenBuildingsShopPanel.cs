using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DoTween
{
    public class OpenBuildingsShopPanel : MonoBehaviour
    {
        [SerializeField] private Button _openBuildingsPanel;
        [SerializeField] private GameObject _buildingsPanel;
        [SerializeField] private Transform _transformBuildingsPanel;

        private void OnEnable()
        {
            _openBuildingsPanel.onClick.AddListener(ShowBuildingsPanel);
        }

        private void ShowBuildingsPanel()
        {
            // так делается секвенция
            Sequence animation = DOTween.Sequence();

            animation
                .Append(_transformBuildingsPanel.DOScale(new Vector3(1, 1, 1), 1f)).SetEase(Ease.OutBounce);


            _buildingsPanel.SetActive(true);
        }
    }
}