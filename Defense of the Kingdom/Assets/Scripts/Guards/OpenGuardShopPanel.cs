using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class OpenGuardShopPanel : MonoBehaviour
{
    [SerializeField] private Button _BuyGuardButton;
    [SerializeField] private GameObject _guardBuyPanel;
    [FormerlySerializedAs("_transformPanel")] [SerializeField] private Transform _transformOpenPanel;
    
    

    private void OnEnable()
    {
        _BuyGuardButton.onClick.AddListener(ShowBuyMenu);
    }

    public void ShowBuyMenu()
    {
        _transformOpenPanel.DOScale(new Vector3(1,1,1), 1f ).SetEase(Ease.OutBounce);
        _guardBuyPanel.SetActive(true);
    }
}
