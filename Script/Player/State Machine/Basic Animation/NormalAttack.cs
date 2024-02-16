using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : PlayerState
{
    public NormalAttack(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.attackTime = player.attackDuration;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        #region Attack Animation
        if (player.IsGrounded())
        {
            player.rb2d.velocity = new Vector2(0, player.rb2d.velocity.y);
        }

        player.attackTime -= Time.deltaTime;

        player.animator.Play("Attack");
        #endregion

        #region Attack -> Movement
        if (player.attackTime <= 0f)
        {
            player.attackCDtime = player.attackCD;

            player.stateMachine.ChangeState(player.runState);
        }
        #endregion
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
