using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ObjectState
{
    // Update is called once per frame
    private float moveSpeed = 2f;
    private float moveTimer = 0f;
    private float moveDuration = 2f;
    private bool movingRight = true;
    private Transform objectTransform;

    public IdleState(Transform transform)
    {
        objectTransform = transform;
    }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
        moveTimer = moveDuration; // Начать с задержки
    }

    public override void Update()
    {
        // Перемещение объекта влево и вправо
        MoveSideToSide();
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            // После завершения перемещения переключаемся на другое состояние
            // Например, можно перейти к преследованию
            //  StateMachine.ChangeState(new ChasingState());
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }

    private void MoveSideToSide()
    {
        float direction = movingRight ? 1 : -1;
        objectTransform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // Проверяем границы передвижения и меняем направление
        if (objectTransform.position.x > 2f || objectTransform.position.x < -2f)
        {
            movingRight = !movingRight;
        }
    }
}
