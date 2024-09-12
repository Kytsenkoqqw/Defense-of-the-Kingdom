using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OpenGuardShopPanel : MonoBehaviour
{
    [SerializeField] private Button _BuyGuardButton;
    [SerializeField] private GameObject _guardBuyPanel;
    

    private void OnEnable()
    {
        _BuyGuardButton.onClick.AddListener(ShowBuyMenu);
    }

    public void ShowBuyMenu()
    {
        _guardBuyPanel.SetActive(true);
    }
}
