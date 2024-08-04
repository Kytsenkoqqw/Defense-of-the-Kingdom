using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowStartMessage : MonoBehaviour
{
    [SerializeField] private GameObject _messagePanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            _messagePanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            StartCoroutine(DestoryMessagePanel());
        }
    }

    IEnumerator DestoryMessagePanel()
    {
        yield return new WaitForSeconds(2f);
        _messagePanel.SetActive(false);
        Destroy(gameObject);
    }
}
