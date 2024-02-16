using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChase : BossState
{
    private Transform playerTransform;

    private float bossMoveSpeed = 4f;

    public BossChase(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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

        #region Main 
        boss.animator.Play("Run");

        Vector2 moveDirection = (playerTransform.position - boss.transform.position).normalized;

        moveDirection.y = 0f;

        boss.BossMovement(moveDirection * bossMoveSpeed);
        #endregion

        #region Chase -> Attack
        if (boss.isInNormalAttackDistance)
        {
            boss.stateMachine.ChangeState(boss.attackState);
        }
        #endregion
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
