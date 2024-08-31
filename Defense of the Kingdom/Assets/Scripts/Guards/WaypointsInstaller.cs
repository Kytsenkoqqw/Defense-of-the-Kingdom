using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class WaypointsInstaller : MonoInstaller
{
    [SerializeField] private Transform[] _waypoints;
    
    public override void InstallBindings()
    {
        Container.Bind<Transform[]>().FromInstance(_waypoints).AsSingle();
    }
}
