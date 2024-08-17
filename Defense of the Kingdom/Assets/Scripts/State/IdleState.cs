using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ObjectState
{
    [SerializeField] private Transform[] waypoints;
    private float moveSpeed = 2f;
    private Transform objectTransform;
    private Animator animator;
    private int currentWaypointIndex = 0;
    private float waitTime = 2f; // Время ожидания на точке
    private float waitTimer = 0f;
    private bool waiting = false; // Флаг ожидания

    public IdleState(Transform transform, Animator animator, Transform[] waypoints)
    {
        objectTransform = transform;
        this.animator = animator;
        this.waypoints = waypoints;

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Waypoints array is null or empty!");
        }
    }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
        MoveToNextWaypoint();
        PlayRunAnimation(true); // Включаем анимацию бега
    }

    public override void Update()
    {
        if (waiting)
        {
            HandleWaiting();
        }
        else
        {
            MoveTowardsCurrentWaypoint();
            CheckAndHandleWaypointArrival();
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
        PlayRunAnimation(false); // Останавливаем анимацию бега при выходе из состояния
    }

    private void MoveTowardsCurrentWaypoint()
    {
        if (waypoints.Length == 0) return;

        Vector2 direction = (waypoints[currentWaypointIndex].position - objectTransform.position).normalized;
        objectTransform.Translate(direction * moveSpeed * Time.deltaTime);

        // Поворот в сторону движения
        objectTransform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
    }

    private void CheckAndHandleWaypointArrival()
    {
        if (waypoints.Length == 0) return;

        if ((waypoints[currentWaypointIndex].position - objectTransform.position).sqrMagnitude < 0.01f)
        {
            StartWaiting(); // Начинаем ожидание
        }
    }

    private void HandleWaiting()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0)
        {
            waiting = false; // Завершаем ожидание
            MoveToNextWaypoint(); // Переходим к следующей точке
        }
    }

    private void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        PlayRunAnimation(true);
    }

    private void StartWaiting()
    {
        PlayRunAnimation(false);
        waiting = true; // Устанавливаем флаг ожидания
        waitTimer = waitTime; // Сбрасываем таймер
    }

    private void PlayRunAnimation(bool play)
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", play);
        }
    }
}
