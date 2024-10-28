using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SelectPawnAction : MonoBehaviour
{
    [SerializeField] private GameObject _choisePanel;
    [SerializeField] private Transform _transformChoisePanel;
    [SerializeField] private Transform _buildButton;
    [SerializeField] private Transform _repairButton;
    
    private void OnMouseEnter()
    {
        _transformChoisePanel.DOScale(new Vector3(0.01f, 0.01f, 0), 0.5f);
        _repairButton.DOMoveX(0.2f, 0.5f);
        _buildButton.DOMoveX(-0.2f, 0.5f);
        _choisePanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        _transformChoisePanel.DOScale(new Vector3(0, 0, 0), 0.5f);
        _repairButton.DOMoveX(0, 0.5f);
        _buildButton.DOMoveX(0, 0.5f);
        _choisePanel.SetActive(false);
    }
}
