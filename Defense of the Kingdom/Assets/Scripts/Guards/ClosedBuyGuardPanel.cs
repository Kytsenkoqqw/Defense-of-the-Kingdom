using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosedBuyGuardPanel : MonoBehaviour
{
    [SerializeField] private Button _closedPanelButton;
    [SerializeField] private GameObject _buyGuardPanel;
    

    private void OnEnable()
    {
        _closedPanelButton.onClick.AddListener(ClosedBuyPanel);
    }

    private void ClosedBuyPanel()
    {
        _buyGuardPanel.SetActive(false);
    }
}
