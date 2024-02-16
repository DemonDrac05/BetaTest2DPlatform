using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLifeState : BossState
{
    public BossLifeState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        if(boss.CurrentHealth > 0f)
        {
            boss.GotDamagedTime = boss.GotDamagedDuration;
        } else boss.DyingTime = boss.DyingDuration;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        #region Hurt State
        if (boss.CurrentHealth > 0f)
        {
            #region Main Function
            boss.GotDamagedTime -= Time.deltaTime;
            boss.animator.Play("Hurt");
            #endregion

            #region Hurt -> Idle
            if (boss.GotDamagedTime <= 0f)
            {
                boss.GotDamagedTime = 0f;
                boss.stateMachine.ChangeState(boss.chaseState);
            }
            #endregion
        }
        #endregion

        #region Death State
        else
        {
            #region Main Function
            boss.bc2d.enabled = false; boss.rb2d.gravityScale = 0f;
            boss.rb2d.velocity = new Vector2(0f, 0f);

            boss.DyingTime -= Time.deltaTime;
            boss.animator.Play("Death");
            #endregion

            #region Death -> Restart Level
            if (boss.DyingTime <= 0f)
            {
                boss.RestartLevel();
            }
            #endregion
        }
        #endregion
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
