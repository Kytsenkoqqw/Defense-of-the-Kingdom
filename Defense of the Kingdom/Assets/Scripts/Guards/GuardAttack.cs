using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAttack : MonoBehaviour
{
    [SerializeField] private Transform _enemyTransform;
    private Animator _animator;
    
    void Update()
    {
        Vector2 direction = _enemyTransform.position - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
        //    if (direction.x > 0)
             //   AttackRight();
         //   else
            //    AttackLeft();
        }
        else
        {
            if (direction.y > 0)
                AttackUp();
            else
                AttackDown();
        }
    }

    void AttackUp()
    {
        _animator.SetTrigger("AttackUp");
        // Логика для удара вверх
    }

    void AttackDown()
    {
        _animator.SetTrigger("AttackDown");
        // Логика для удара вниз
    }

    /*void AttackLeft()
    {
        _animator.SetTrigger("AttackLeft");
        // Логика для удара влево
    }*/

    /*void AttackRight()
    {
        _animator.SetTrigger("AttackRight");
        // Логика для удара вправо
    }*/

}
