using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : BossState
{
    private Transform playerTransform;
    public BossAttack(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void EnterState()
    {
        boss.attackTimer = boss.attackDuration;
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        #region Main
        boss.attackTimer -= Time.deltaTime;
        boss.BossMovement(Vector2.zero);
        boss.animator.Play("Attack");
        #endregion

        #region Attack -> Chase
        if (boss.attackTimer <= 0f)
        {
            boss.stateMachine.ChangeState(boss.chaseState);
        }
        #endregion
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}