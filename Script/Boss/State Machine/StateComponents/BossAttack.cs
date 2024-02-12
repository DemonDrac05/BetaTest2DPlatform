using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : BossState
{
    private Transform playerTransform;

    private float timer;
    private float duration = 1f;
    public BossAttack(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void EnterState()
    {
        timer = duration;
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        timer -= Time.deltaTime;

        boss.BossMovement(Vector2.zero);

        boss.animator.Play("Attack");

        if(timer <= 0f)
        {
            boss.stateMachine.ChangeState(boss.chaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}