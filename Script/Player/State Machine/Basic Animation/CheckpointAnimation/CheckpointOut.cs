using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointOut : PlayerState
{
    private float prayingTimeOut;
    private float prayingTimeOutDuration = 0.75f;
    public CheckpointOut(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        prayingTimeOut = prayingTimeOutDuration;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        prayingTimeOut -= Time.deltaTime;
        player.animator.Play("CheckpointOut");

        if (prayingTimeOut <= 0f) 
        {
            prayingTimeOut = 0f;
            player.stateMachine.ChangeState(player.runState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
