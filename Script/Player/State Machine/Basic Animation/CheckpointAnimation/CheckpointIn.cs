using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointIn : PlayerState
{
    private float prayingTimeIn;
    private float prayingTimeInDuration = 0.75f;
    public CheckpointIn(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        prayingTimeIn = prayingTimeInDuration;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        prayingTimeIn -= Time.deltaTime;
        player.animator.Play("CheckpointIn");

        if(prayingTimeIn <= 0f)
        {
            prayingTimeIn = 0f;
            player.stateMachine.ChangeState(player.cpProgress);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
