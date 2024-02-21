using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointProgress : PlayerState
{
    public CheckpointProgress(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
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

        player.animator.Play("CheckpointProgress");

        if(Input.GetKeyDown(KeyCode.E))
        {
            player.stateMachine.ChangeState(player.cpOut);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

