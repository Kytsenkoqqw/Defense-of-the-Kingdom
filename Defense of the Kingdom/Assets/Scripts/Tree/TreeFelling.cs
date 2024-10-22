using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeFelling : MonoBehaviour
{
    [SerializeField] private GameObject _treeButton;
    [SerializeField] private GameObject _loot;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            _treeButton.SetActive(true);
        }
    }

    public void StartFelling()
    {
        StartCoroutine(FellingTree());
    }

    private IEnumerator FellingTree()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(_loot);
        Destroy(gameObject);
    }
}
