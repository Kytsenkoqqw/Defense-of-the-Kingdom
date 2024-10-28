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

    private void OnMouseDown()
    {
        _choisePanel.SetActive(true);
        _transformChoisePanel.DOScale(new Vector3(0.01f, 0.01f, 0), 0.5f);
        _repairButton.DOLocalMoveX(35, 0.5f);
        _buildButton.DOLocalMoveX(-35, 0.5f);
    }

    private void OnMouseExit()
    {
        _transformChoisePanel.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() =>
        {
            _choisePanel.SetActive(false);
        });
        _repairButton.DOMoveX(0, 0.5f);
        _buildButton.DOMoveX(0, 0.5f);
    }
}
