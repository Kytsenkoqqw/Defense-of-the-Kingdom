using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class AttackAreasInstaller : MonoInstaller
{
    [SerializeField] private PolygonCollider2D[] _attackAreas;

    public override void InstallBindings()
    {
        Container.Bind<Collider2D[]>().FromInstance(_attackAreas).AsSingle();
    }

}
