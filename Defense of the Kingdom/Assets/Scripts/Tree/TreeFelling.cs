using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TreeFelling : MonoBehaviour
{
    [SerializeField] private GameObject _treeButton;
    [SerializeField] private Transform _treeTransform;
    [SerializeField] private GameObject _loot;
    [SerializeField] private AudioSource _fallingTreeFx;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<AttackArea>())
        {
            Debug.Log("Hit to tree");
            StartFelling();
        }
    }

    private void FallingTree()
    {
        Sequence animationTree = DOTween.Sequence();
        animationTree
            .Append(_treeTransform.DOPunchPosition(Vector3.right * 0.1F, 1f))
            .Append(_treeTransform.DORotate(new Vector3(0, 0, 90), 1).SetEase(Ease.Linear))
            .Append(_treeTransform.DORotate(new Vector3(0, 0, 85), 0.2f).SetEase(Ease.Linear))
            .Append(_treeTransform.DORotate(new Vector3(0, 0, 90), 0.2f).SetEase(Ease.Linear));
    }

    public void StartFelling()
    {
        StartCoroutine(FellingTree());
    }

    private IEnumerator FellingTree()
    {
        _fallingTreeFx.Play();
        FallingTree();
        yield return new WaitForSeconds(3f);
        Instantiate(_loot,_treeTransform.position, Quaternion.identity);
      
        Destroy(gameObject);
    }
}
