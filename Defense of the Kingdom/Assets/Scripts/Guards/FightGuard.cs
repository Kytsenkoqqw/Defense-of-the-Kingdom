using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGuard : MonoBehaviour
{
    private GuardBehaviour _guardBehaviour;

    private void OnEnable()
    {
        GuardBehaviour.OnFighting += Fight;
    }

    void Start()
    {
        _guardBehaviour = GetComponent<GuardBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        GuardBehaviour.OnFighting -= Fight;
    }

    private void Fight()
    {
        _guardBehaviour.SetSpeed(0);
    }
}
