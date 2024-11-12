using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _attackArea;
    private Animator _animator;

    private void Start()
    {
        _attackArea.SetActive(false);
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        FrontAttack();
    }

    private void FrontAttack()
    {
        // Проверка, чтобы атака выполнялась только если курсор не над UI элементом
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            StartCoroutine(OnAttackArea());
            _animator.SetTrigger("Fight");
        }
    }

    IEnumerator OnAttackArea()
    {
        yield return new WaitForSeconds(0.40f);
        _attackArea.SetActive(true);
        yield return new WaitForSeconds(0.3f); // Время, на которое активируется область атаки
        _attackArea.SetActive(false);
    }
    
    
}
