using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClosedBuyGuardPanel : MonoBehaviour
{
    [SerializeField] private Button _closedPanelButton;
    [SerializeField] private GameObject _buyGuardPanel;
    [SerializeField] private Transform _transformClosedPanel;
    
    private void OnEnable()
    {
        _closedPanelButton.onClick.AddListener(ClosedBuyPanel);
    }

    private void ClosedBuyPanel()
    {
        _transformClosedPanel.DOScale(new Vector3(0, 0, 0), 0.5f);
        _buyGuardPanel.SetActive(false);
    }
}
