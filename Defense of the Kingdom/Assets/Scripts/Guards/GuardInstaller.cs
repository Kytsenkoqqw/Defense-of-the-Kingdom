using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GuardInstaller : MonoInstaller
{
    public PolygonCollider2D upAttackArea;
    public PolygonCollider2D frontAttackArea;
    public PolygonCollider2D downAttackArea;
    public Transform[] waypoints;

    public override void InstallBindings()
    {
        Container.Bind<PolygonCollider2D>().WithId("UpAttack").FromInstance(upAttackArea).AsSingle();
        Container.Bind<PolygonCollider2D>().WithId("FrontAttack").FromInstance(frontAttackArea).AsSingle();
        Container.Bind<PolygonCollider2D>().WithId("DownAttack").FromInstance(downAttackArea).AsSingle();
        Container.Bind<Transform[]>().FromInstance(waypoints).AsSingle();
    }
}
