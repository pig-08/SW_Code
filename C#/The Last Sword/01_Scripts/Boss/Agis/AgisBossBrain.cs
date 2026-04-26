using Gwamegi.Code.Entities;
using SW.Code.Boss;
using UnityEngine;

public class AgisBossBrain : BossBrain
{
    public AgisBossAnimation AgisBossAnimationCompo { get { return bossAnimation; } }
    private AgisBossAnimation bossAnimation;

    public void AngisInit()
    {
        bossAnimation = GetComponentInChildren<AgisBossAnimation>();
        base.Init();
    }

    protected override void HandleDead()
    {

    }

    protected override void HandleHit()
    {

    }
}
