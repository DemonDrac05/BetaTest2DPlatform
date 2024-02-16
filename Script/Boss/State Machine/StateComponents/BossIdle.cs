using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BossState
{
    public BossIdle(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        boss.animator.Play("Idle");

        if(boss.isAggroed && boss.player.CurrentHealth > 0f)
        {
            stateMachine.ChangeState(boss.chaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
